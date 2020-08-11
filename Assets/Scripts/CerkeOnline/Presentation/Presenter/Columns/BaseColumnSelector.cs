using System.Linq;
using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    public abstract class BaseColumnSelector : MonoBehaviour
    {
        [SerializeField] IReadOnlyPieceUnityEvent onPieceSelected = default;
        [SerializeField] IntegerVector2UnityEvent onViaPositionSelected = default;
        [SerializeField] IntegerVector2UnityEvent onTargetPositionSelected = default;

        static readonly IntegerVector2 NonePosition = new IntegerVector2(-1, -1);
        IntegerVector2 startPosition = NonePosition;
        IntegerVector2 viaPosition = NonePosition;
        protected bool isLockSelecting = false;

        MovePredictor movePredictor;
        MovePredictor MovePredictor 
        {
            get 
            {
                if (movePredictor == null)
                {
                    var gameController = Application.GameController.Instance;
                    movePredictor = new MovePredictor(gameController.Game.Board);
                    gameController.OnGameReset.Subscribe(game => movePredictor = new MovePredictor(game.Board));
                }
                return movePredictor; 
            }
        }
        
        public void OnClickColumn(IntegerVector2 position)
        {
            if (isLockSelecting || this.startPosition == NonePosition || position == NonePosition)
            {
                this.startPosition = position;
                this.viaPosition = NonePosition;

                CallPieceSelectedEvent(position);
                
                return;
            }

            if (this.viaPosition == NonePosition)
            {
                //経由点への到達可能性チェック
                var game = Application.GameController.Instance.Game;
                var board = game.Board;
                var piece = board.GetPiece(startPosition);
                if (MovePredictor.PredictMoveableColumns(startPosition, piece).Count(value => value == position) == 0)
                {
                    this.startPosition = NonePosition;
                    CallPieceSelectedEvent(NonePosition);
                    return;
                }

                //座標の更新
                this.viaPosition = position;

                //終点として指定されたのではないなら経由点が指定されたことをイベントで通知
                if(board.GetPiece(viaPosition) != null)
                    CallViaPositionSelectedEvent(position);

                return;
            }

            onTargetPositionSelected.Invoke(position);
            OnColumnSelected(this.startPosition, this.viaPosition, position);
            this.startPosition = NonePosition;
        }

        protected abstract void OnColumnSelected(IntegerVector2 start, IntegerVector2 via, IntegerVector2 last);

        void CallPieceSelectedEvent(IntegerVector2 position)
        {
            var game = Application.GameController.Instance.Game;
            var board = game.Board;
            var piece = board.GetPiece(position);
            onPieceSelected.Invoke(piece);
        }
        
        void CallViaPositionSelectedEvent(IntegerVector2 position)
        {
            onViaPositionSelected.Invoke(position);
        }
    }

    [System.Serializable] public class IntegerVector2UnityEvent : UnityEngine.Events.UnityEvent<IntegerVector2> { }
}
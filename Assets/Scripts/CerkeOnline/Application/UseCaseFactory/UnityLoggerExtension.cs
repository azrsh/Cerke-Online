using Azarashi.CerkeOnline.Domain.UseCase;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.Application
{
    internal static class UnityLoggerExtension
    {
        public static Domain.UseCase.ILogger ToUseCaseLogger(this UnityEngine.ILogger logger)
        {
            return new LoaggerWrapper(logger);
        }

        private class LoaggerWrapper : Domain.UseCase.ILogger
        {
            readonly UnityEngine.ILogger logger;

            public LoaggerWrapper(UnityEngine.ILogger logger)
            {
                this.logger = logger;
            }

            public void Log(Domain.UseCase.ISystemMessage message) => logger.Log(SystemMessageTranslator.Translate(message));
        }

        private static class SystemMessageTranslator
        {
            //全て手動. 汚い.
            public static string Translate(ISystemMessage systemMessage)
            {
                var type = systemMessage.GetType();
                var translator = LanguageManager.Instance.Translator;

                if (type == typeof(NotYourTurnMessage))
                {
                    return translator.Translate(TranslatableKeys.NotYourTurnMessage);
                }
                else if (type == typeof(NotYourPieceMessage))
                {
                    return translator.Translate(TranslatableKeys.NotYourPieceMessage);
                }
                else if (type == typeof(PieceMovementMessage))
                {
                    //Player = player;
                    //Piece = piece;
                    //StartPosition = startPosition;
                    //EndPosition = endPosition;

                    var message = (PieceMovementMessage)systemMessage;
                    return translator.Translate(TranslatableKeys.PieceMovementMessage)
                        .Replace("#PIECE_NAME#", message.Piece.Name.ToString())
                        .Replace("#START_POSITION#", message.StartPosition.ToString())
                        .Replace("#END_POSITION#", message.EndPosition.ToString());
                }
                else if (type == typeof(PieceViaMovementMessage))
                {
                    //Player = player;
                    //Piece = piece;
                    //StartPosition = startPosition;
                    //ViaPosition = viaPosition;
                    //EndPosition = endPosition;

                    var message = (PieceViaMovementMessage)systemMessage;
                    return translator.Translate(TranslatableKeys.PieceViaMovementMessage)
                        .Replace("#PIECE_NAME#", message.Piece.Name.ToString())
                        .Replace("#START_POSITION#", message.StartPosition.ToString())
                        .Replace("#VIA_POSITION#", message.ViaPosition.ToString())
                        .Replace("#END_POSITION#", message.EndPosition.ToString());
                }
                else if (type == typeof(PieceMovementFailureMessage))
                {
                    return translator.Translate(TranslatableKeys.PieceMovementFailureMessage);
                }
                else if (type == typeof(NoPieceViaPointMessage))
                {
                    return translator.Translate(TranslatableKeys.NoPieceViaPointMessage);
                }
                else if (type == typeof(NoPieceSelectedMessage))
                {
                    return translator.Translate(TranslatableKeys.NoPieceSelectedMessage);
                }
                else if (type == typeof(SetPieceMessage))
                {
                    //Player = player;
                    //Piece = piece;
                    //SetPosition = setPosition;

                    var message = (SetPieceMessage)systemMessage;
                    return translator.Translate(TranslatableKeys.SetPieceMessage)
                        .Replace("#PIECE_NAME#", message.Piece.Name.ToString())
                        .Replace("#POSITION#", message.SetPosition.ToString()); 
                }
                else if (type == typeof(SetPieceFailureMessage))
                {
                    return translator.Translate(TranslatableKeys.SetPieceFailureMessage);
                }

                return string.Empty;
            }
        }
    }
}
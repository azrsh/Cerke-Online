using UnityEngine;
using Azarashi.Utilities.Collections;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{ 
    public class ColumnPositionConverter
    {
        readonly Vector2ArrayAccessor<Transform> columns;

        public ColumnPositionConverter(IMapProvider<Transform> mapProvider)
        {
            var transformMap = mapProvider.GetMap();
            columns = new Vector2ArrayAccessor<Transform>(transformMap);
        }

        public Vector3 Convert(Vector2Int logicPosition)
        {
            //Vector2ArrayAccessorでxyが入れ替わってる
            logicPosition = new Vector2Int(logicPosition.y, logicPosition.x);
            return columns.Read(logicPosition).position;
        }
    }
}
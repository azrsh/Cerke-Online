using UnityEngine;
using Azarashi.Utilities.Collections;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{ 
    public class ColumnPositionConverter
    {
        readonly Vector2XYArrayAccessor<Transform> columns;

        public ColumnPositionConverter(IMapProvider<Transform> mapProvider)
        {
            var transformMap = mapProvider.GetMap();
            columns = new Vector2XYArrayAccessor<Transform>(transformMap);
        }

        public Vector3 Convert(Vector2Int logicPosition)
        {
            return columns.Read(logicPosition).position;
        }
    }
}
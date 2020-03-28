using UnityEngine;
using Azarashi.Utilities.Collections;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

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

        public Vector3 Convert(IntVector2 logicPosition)
        {
            return columns.Read(new Vector2Int(logicPosition.x, logicPosition.y)).position;
        }
    }
}
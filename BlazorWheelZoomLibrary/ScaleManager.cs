using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor
{
    public class ScaleManager
    {
        public ScaleManager()
        {
            m_scale = 1;
        }

        // The scale
        double m_scale;
        public double Scale => m_scale;
        // The document size
        public double DocSize { get; set; }
        // The view size
        public double ViewSize { get; set; }
        // The current view offset, for example if ViewOffset = -1 and Scale = 1 then firts document column is not visible
        double m_viewOffset;
        public double ViewOffset => m_viewOffset;

        public void SetViewOffset(double offset)
        {
            m_viewOffset = Math.Min(0, Math.Max(-ViewRange, offset));
        }

        /// <summary>
        /// Find offset to have a point at the correct ditance from border
        /// </summary>
        /// <param name="desiredPoint">In image point at scale 1</param>
        /// <param name="desiredDistanceFromBorder">In view point from left border</param>
        public void ComputeOffset(double desiredPoint, double desiredDistanceFromBorder)
        {
            double finalPoint = desiredPoint * Scale;
            SetViewOffset(desiredDistanceFromBorder - finalPoint);
        }

        public void SetScale(double scale)
        {
            m_scale = scale;
            SetViewOffset(m_viewOffset); // Check image stay in offset range
        }

        /// Get the view range in document unit. 
        /// If zero then view is bigger than document.
        public double ViewRange 
        { 
            get
            {
                double realDocSize = DocSize * Scale;
                double viewRange = realDocSize - ViewSize;
                if (viewRange < 0)
                    viewRange = 0;
                return viewRange;
            }
        }

        public double ActualViewSize
        {
            get
            {
                return DocSize * Scale;
            }
        }

        /// <summary>
		/// Convert a view point to a document point, for example for mouse clic
        /// </summary>
        /// <param name="pos">Point in view reference</param>
        /// <returns>Point in doc reference</returns>
        public double ViewToDoc(double pos)
        {
            return (pos - ViewOffset) / Scale;
        }

        /// <summary>
		/// Convert a document point to a view point, for example to draw on the image at the correct scale
        /// </summary>
        /// <param name="pos">Point in doucment reference</param>
        /// <returns>Point in view reference</returns>
        public double DocToView(double pos)
        {
            return pos * Scale + ViewOffset;
        }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}

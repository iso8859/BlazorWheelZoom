using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BlazorWheelZoomLibrary
{
    public class Rectangle
    {
        static public readonly string _white = "#FFFFFF";
        static public readonly string _black = "#000000";
        static public readonly string _none = "none";

        public double left, top, right, bottom;
        public string color = _black;
        public string fill = _none;
        public string fill_opacity = "1";
        public string Factor(double zoom, double val, double offset)
        {
            string tmp = (offset+(val * zoom)).ToStringInvariant();
            return tmp;
        }
        public string GetSVG(double xoffset, double yoffset, double zoom)
        {
            //var tmp = $"<rect width='{Factor(zoom, right - left)}' height='{Factor(zoom, bottom - top)}' fill='{fill}' stroke='{color}' stroke-width='1'/>";
            var tmp = $"<g clip-path='url(#clip-path)' transform='translate({Factor(zoom, left, xoffset)},{Factor(zoom, top, yoffset)})'><rect width='{Factor(zoom, right - left, 0)}' height='{Factor(zoom, bottom - top, 0)}' fill='{fill}' fill-opacity='{fill_opacity}' stroke='{color}' stroke-width='1'/></g>";
            Console.WriteLine(tmp);
            return tmp;
        }
    }

}

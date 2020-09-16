using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor
{
    public class HtmlLenght
    {
        public double val { get; set; }
        public string unit { get; set; } = "px";

        static public HtmlLenght Parse(string coord)
        {
            HtmlLenght result = new HtmlLenght() { val = 0, unit = "" };
            string accumulator = "";
            foreach (char c in coord)
            {
                if (c != ' ')
                {
                    if (char.IsDigit(c) || c == '.' || c==',')
                        accumulator += c;
                    else
                        result.unit += c;
                }
            }
            if (accumulator.Length > 0)
            {
                double v = 0;
                if (double.TryParse(accumulator.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out v))
                    result.val = v;
            }
            if (result.unit.Length == 0)
                result.unit = "px";
            return result;
        }

        public override string ToString()
        {
            return $"{val.ToString(System.Globalization.CultureInfo.InvariantCulture)}{unit}";
        }
    }

    public class Coord
    {
        public HtmlLenght x { get; set; }
        public HtmlLenght y { get; set; }

        public Coord()
        {

        }

        public Coord(double x, double y)
        {
            this.x = new HtmlLenght() { val = x };
            this.y = new HtmlLenght() { val = y };
        }

        public override string ToString()
        {
            return x.ToString() + ' ' + y.ToString();
        }
    }
    // style="background-repeat: no-repeat; background-image: url("file:///C:/temp/wheelzoom-master/daisy.jpg"); background-size: 555px 320px; background-position: 0px 0px;">
    public class HtmlStyleHelper : Dictionary<string, List<string>>
    {
        public static HtmlStyleHelper Parse(string source)
        {
            HtmlStyleHelper result = new HtmlStyleHelper();
            string[] items = source.Split(';');
            string lastItem = "";
            foreach (string item in items)
            {
                int idx = item.IndexOf(':');
                if (idx != -1)
                {
                    lastItem = item.Substring(0, idx).Trim();
                    result[lastItem] = item.Substring(idx + 1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(_ => _.Trim()).ToList();
                }
                else
                {
                    if (item.StartsWith("base64,") && !string.IsNullOrEmpty(lastItem))
                        result[lastItem][0] += ";" + item;
                }
            }
            return result;
        }

        public void SetOne(string attribute, string val)
        {
            this[attribute] = new List<string>() { val };
        }

        public string GetOne(string attribute)
        {
            string result = "";
            if (ContainsKey(attribute))
            {
                var lst = this[attribute];
                if (lst != null && lst.Count > 0)
                    result = lst[0];
            }    
            return result;
        }

        public override string ToString()
        {
            string result = "";
            foreach (var item in this)
                result += item.Key + ": " + string.Join(" ", item.Value) + ";";
            return result;
        }

        public Coord getCoord(string attribName)
        {
            Coord result = new Coord();
            if (ContainsKey(attribName))
            {
                if (this[attribName].Count == 2)
                {
                    result.x = HtmlLenght.Parse(this[attribName][0]);
                    result.y = HtmlLenght.Parse(this[attribName][1]);
                }
            }
            return result;
        }

        public void setCoord(string attribName, Coord val)
        {
            this[attribName] = new List<string>() { val.x.ToString(), val.y.ToString() };
        }
    }
    public class HtmlAttributeHelper : Dictionary<string, object>
    {
        public HtmlAttributeHelper(Dictionary<string, object> src) : base(src)
        {
            
        }
        public HtmlStyleHelper style
        {
            get
            {
                if (!ContainsKey("style") || this["style"] == null)
                    this["style"] = "";
                return HtmlStyleHelper.Parse(this["style"]?.ToString());
            }
            set
            {
                this["style"] = value.ToString();
            }
        }
    }
}

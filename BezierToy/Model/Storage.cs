using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BezierToy
{
    partial class Model
    {
        public void Save(string filename)
        {
            using (XmlTextWriter tw = new XmlTextWriter(filename, Encoding.ASCII))
            {
                tw.Formatting = Formatting.Indented;
                tw.Indentation = 4;
                tw.WriteStartDocument(true);
                tw.WriteStartElement("bezier-toy");

                tw.WriteStartElement("base-curve");
                tw.WriteAttributeString(
                    "color",
                    BaseCurve.Color.ToArgb().ToString("X8")
                );
                foreach (Vector2D point in BaseCurve.Points)
                {
                    tw.WriteStartElement("point");
                    tw.WriteAttributeString(
                        "x",
                        point.X.ToString(CultureInfo.InvariantCulture)
                    );
                    tw.WriteAttributeString(
                        "y",
                        point.Y.ToString(CultureInfo.InvariantCulture)
                    );
                    tw.WriteEndElement();
                }
                tw.WriteEndElement();

                foreach (ReducedBezierCurve curve in ReducedCurves)
                {
                    tw.WriteStartElement("reduced-curve");
                    tw.WriteAttributeString(
                        "method",
                        Reducers.Find(r => r.Factory.CanProduce(curve.Reducer))
                            .XmlName
                    );
                    tw.WriteAttributeString("degree", curve.Degree.ToString());
                    tw.WriteAttributeString(
                        "color",
                        curve.Color.ToArgb().ToString("X8")
                    );
                    curve.Reducer.WriteCustomAttributes(tw);
                    tw.WriteEndElement();
                }

                tw.WriteEndElement();
                tw.WriteEndDocument();
            }
        }

        public void Load(string filename)
        {
            // We assume model is clear.

            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            BaseCurve.Points.Clear();
            XmlNode baseCurveNode = doc.SelectSingleNode("bezier-toy/base-curve");
            BaseCurve.Color = Color.FromArgb(int.Parse(
                baseCurveNode.Attributes["color"].Value,
                NumberStyles.HexNumber
            ));
            foreach (XmlNode pointNode in baseCurveNode.SelectNodes("point"))
                BaseCurve.Points.Add(new Vector2D(
                    double.Parse(
                        pointNode.Attributes["x"].Value,
                        CultureInfo.InvariantCulture
                    ),
                    double.Parse(
                        pointNode.Attributes["y"].Value,
                        CultureInfo.InvariantCulture
                    )
                ));

            SelectedCurve = null;
            ReducedCurves.Clear();
            foreach (XmlNode reducedCurveNode in doc.SelectNodes("bezier-toy/reduced-curve"))
            {
                ReducerRecord record = Reducers.Find(
                    r => r.XmlName == reducedCurveNode.Attributes["method"]
                        .Value
                );
                Reducer reducer = record.Factory.Produce();
                reducer.ReadCustomAttributes(reducedCurveNode);
                ReducedBezierCurve curve = new ReducedBezierCurve(
                    BaseCurve, reducer);
                curve.Degree = int.Parse(
                    reducedCurveNode.Attributes["degree"].Value);
                curve.Color = Color.FromArgb(int.Parse(
                    reducedCurveNode.Attributes["color"].Value,
                    NumberStyles.HexNumber
                ));
                ReducedCurves.Add(curve);
            }

            FileName = filename;
        }

        public void Export(string filename, ImageFormat format)
        {
            Control canvas = MainWindow.Instance.Canvas;
            using (Bitmap bitmap = new Bitmap(canvas.Width, canvas.Height))
            {
                canvas.DrawToBitmap(
                    bitmap,
                    new Rectangle(0, 0, canvas.Width, canvas.Height)
                );
                bitmap.Save(filename, format);
            }
        }
    }

    partial class ConstrainedReducer
    {
        public override void WriteCustomAttributes(XmlTextWriter tw)
        {
            tw.WriteAttributeString("continuity-0",
                ContinuityClassAt0.ToString());
            tw.WriteAttributeString("continuity-1",
                ContinuityClassAt1.ToString());
        }

        public override void ReadCustomAttributes(XmlNode node)
        {
            ContinuityClassAt0 = int.Parse(
                node.Attributes["continuity-0"].Value);
            ContinuityClassAt1 = int.Parse(
                node.Attributes["continuity-1"].Value);
        }
    }
}

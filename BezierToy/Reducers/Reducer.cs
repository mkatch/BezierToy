using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BezierToy
{
    public abstract class Reducer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(
                    this,
                    new PropertyChangedEventArgs(propertyName)
                );
        }

        private string label = "Generic reducer";
        public string Label
        {
            get { return label; }
            protected set
            {
                if (label != value)
                {
                    label = value;
                    OnPropertyChanged("Label");
                }
            }
        }

        public abstract bool Reduce(Vector2D[] basePoints, Vector2D[] reducedPoints);

        public virtual void WriteCustomAttributes(XmlTextWriter tw)
        {
            // Do nothing.
        }

        public virtual void ReadCustomAttributes(XmlNode node)
        {
            // Do nothing.
        }
    }

    public interface IReducerFactory
    {
        Reducer Produce();

        bool CanProduce(Reducer reducer);
    }

    public class ReducerFactory<T> : IReducerFactory where T : Reducer, new()
    {
        public Reducer Produce()
        {
            return new T();
        }

        public bool CanProduce(Reducer reducer)
        {
            return typeof(T).Equals(reducer.GetType());
        }
    }

    public class ReducerRecord
    {
        public string Name { get; set; }
        public string XmlName { get; set; }
        public IReducerFactory Factory { get; set; }
        public ReducerView Control { get; set; }

        public ReducerRecord(
            string name,
            string xmlName,
            IReducerFactory factory,
            ReducerView control
        )
        {
            Name = name;
            XmlName = xmlName;
            Factory = factory;
            Control = control;
        }
    }
}

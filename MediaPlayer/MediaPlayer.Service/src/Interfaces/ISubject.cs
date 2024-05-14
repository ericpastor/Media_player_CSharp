using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.src.Interfaces;

namespace MediaPlayer.Service.src.Interfaces
{
    public interface ISubject
    {
        void Notify(string message);
        void Subscribe(IObserver observer);
        void UnSubscribe(IObserver observer);
    };
}
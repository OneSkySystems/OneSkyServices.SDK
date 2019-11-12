using System;
namespace OneSky.Services
{
    public interface INavigationError
    {
        DateTime Time { get; set; }
        double HorizontalError { get; set; }
        double VerticalError { get; set; }
        double TimeError { get; set; }
        double PositionError { get; set; }
        double XError { get; set; }
        double YError { get; set; }
    }

}
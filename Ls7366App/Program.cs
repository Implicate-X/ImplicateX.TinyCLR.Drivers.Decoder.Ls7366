using System;
using GHIElectronics.TinyCLR.Pins;
using GHIElectronics.TinyCLR.Devices.Gpio;
using ImplicateX.Drivers.Decoder;

namespace Ls7366App
{
	internal class Program
	{
		public static class ControllerPin
		{
			public const int DecoderSCLK = FEZFeather.GpioPin.PC8;
			public const int DecoderMOSI = FEZFeather.GpioPin.PC9;
			public const int DecoderMISO = FEZFeather.GpioPin.PC10;
			public const int DecoderCS = FEZFeather.GpioPin.PC11;
		}

		private static GpioController gpioController_;
		private static Ls7366 quadDecoder_;

		static void Main()
		{
			gpioController_ = GpioController.GetDefault();

			quadDecoder_ = new(
				gpioController_,
				ControllerPin.DecoderCS,
				ControllerPin.DecoderSCLK,
				ControllerPin.DecoderMOSI,
				ControllerPin.DecoderMISO );

			quadDecoder_.CounterChanged += ( e ) =>
			{
				Console.WriteLine( $"Counter: {e.Count}, Direction: {e.Direction}, Status: {e.Status}" );
			};

			quadDecoder_.ResetCounter();
			quadDecoder_.Start();
		}
	}
}

namespace System {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Diagnostics.CodeAnalysis;

    internal static class Assert {
        public static class Argument {

            public static void Valid(FormattableString message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new ArgumentException( message: message.ToString() );
            }

            public static void NotNull(FormattableString message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new ArgumentNullException( null, message: message.ToString() );
            }

            public static void InRange(FormattableString message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new ArgumentOutOfRangeException( null, message: message.ToString() );
            }

        }
        public static class Operation {

            public static void Valid(FormattableString message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new InvalidOperationException( message: message.ToString() );
            }

            public static void Ready(FormattableString message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new InvalidOperationException( message: message.ToString() );
            }

            public static void NotDisposed(FormattableString message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new ObjectDisposedException( null, message: message.ToString() );
            }

        }
    }
}

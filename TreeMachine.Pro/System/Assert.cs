namespace System {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    internal static class Assert {
        public static class Argument {

            public static void Valid(string message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new ArgumentException( message: message );
            }

            public static void NotNull(string message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new ArgumentNullException( null, message: message );
            }

            public static void InRange(string message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new ArgumentOutOfRangeException( null, message: message );
            }

        }
        public static class Operation {

            public static void Valid(string message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new InvalidOperationException( message: message );
            }

            public static void Ready(string message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new InvalidOperationException( message: message );
            }

            public static void NotDisposed(string message, [DoesNotReturnIf( false )] bool condition) {
                if (!condition) throw new ObjectDisposedException( null, message: message );
            }

        }
    }
}

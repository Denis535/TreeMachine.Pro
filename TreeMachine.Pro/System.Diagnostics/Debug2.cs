namespace System.Diagnostics {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    internal static class Debug2 {
        public static class Assert {
            public static class Argument {

                [Conditional( "DEBUG" )]
                public static void Valid(string message, [DoesNotReturnIf( false )] bool condition) {
                    Debug.Assert( condition, message );
                }

                [Conditional( "DEBUG" )]
                public static void NotNull(string message, [DoesNotReturnIf( false )] bool condition) {
                    Debug.Assert( condition, message );
                }

                [Conditional( "DEBUG" )]
                public static void InRange(string message, [DoesNotReturnIf( false )] bool condition) {
                    Debug.Assert( condition, message );
                }

            }
            public static class Operation {

                [Conditional( "DEBUG" )]
                public static void Valid(string message, [DoesNotReturnIf( false )] bool condition) {
                    Debug.Assert( condition, message );
                }

                [Conditional( "DEBUG" )]
                public static void Ready(string message, [DoesNotReturnIf( false )] bool condition) {
                    Debug.Assert( condition, message );
                }

                [Conditional( "DEBUG" )]
                public static void NotDisposed(string message, [DoesNotReturnIf( false )] bool condition) {
                    Debug.Assert( condition, message );
                }

            }
        }
    }
}

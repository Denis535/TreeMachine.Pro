namespace System.Diagnostics {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    internal static class Debug2 {
        internal static class Assert {

            [Conditional( "DEBUG" )]
            public static void Argument(string message, [DoesNotReturnIf( false )] bool condition) {
                Debug.Assert( condition, message );
            }

            [Conditional( "DEBUG" )]
            public static void Operation(string message, [DoesNotReturnIf( false )] bool condition) {
                Debug.Assert( condition, message );
            }

            [Conditional( "DEBUG" )]
            public static void Internal(string message, [DoesNotReturnIf( false )] bool condition) {
                Debug.Assert( condition, message );
            }

        }
    }
}

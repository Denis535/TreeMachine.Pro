namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public abstract class ComponentMachineBase {

    // RootComponent
    protected ComponentBase? RootComponent { get; private set; }

    // Constructor
    public ComponentMachineBase() {
    }

    // SetRootComponent
    protected virtual void SetRootComponent(ComponentBase component, object? argument = null) {
        Assert.Argument.Message( $"Argument 'component' must be non-null" ).NotNull( component != null );
        Assert.Argument.Message( $"Argument 'component' ({component}) must be inactive" ).Valid( component.State is ComponentBase.State_.Inactive );
        Assert.Operation.Message( $"ComponentSystem {this} must have no root component" ).Valid( RootComponent == null );
        {
            RootComponent = component;
            RootComponent.Owner = this;
        }
        RootComponent.Activate( argument );
    }
    protected internal virtual void RemoveRootComponent(ComponentBase component, object? argument = null) {
        Assert.Argument.Message( $"Argument 'component' must be non-null" ).NotNull( component != null );
        Assert.Operation.Message( $"ComponentSystem {this} must have {component} root component" ).Valid( RootComponent == component );
        RemoveRootComponent( argument );
    }
    protected virtual void RemoveRootComponent(object? argument = null) {
        Assert.Operation.Message( $"ComponentSystem {this} must have root component" ).Valid( RootComponent != null );
        RootComponent.Deactivate( argument );
        {
            RootComponent.Owner = null;
            RootComponent = null;
        }
    }

}

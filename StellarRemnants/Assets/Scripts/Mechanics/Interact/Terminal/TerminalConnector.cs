using System.Collections.Generic;

namespace StellarRemnants.Interact {
    public class TerminalConnector {
        public List<TerminalField> Fields;

        public TerminalConnector(params TerminalField[] fields) {

            // TODO: Enabled/Disabled and Powered/Unpowered should be implicit. All IntegratedInteractables will have this option.
            this.Fields = new List<TerminalField>(fields);


        }
    }
}
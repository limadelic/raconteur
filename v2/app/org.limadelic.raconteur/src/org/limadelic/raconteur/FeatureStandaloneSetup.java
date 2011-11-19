
package org.limadelic.raconteur;

/**
 * Initialization support for running Xtext languages 
 * without equinox extension registry
 */
public class FeatureStandaloneSetup extends FeatureStandaloneSetupGenerated{

	public static void doSetup() {
		new FeatureStandaloneSetup().createInjectorAndDoEMFRegistration();
	}
}


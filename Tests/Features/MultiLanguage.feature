Feature: Multilingual
	Allow to write Features in multiple languages

Scenario: Que viva España, y Olé!

	Select language "es"

	Given the Feature is
	"
		Funcionalidad: Multilingue

		Escenario: En Español
	"

	The Runner should contain
	"
		[TestClass]
		public partial class Multilingue
		{
			[TestMethod]
			public void EnEspañol()
			{
			}
		}
	"

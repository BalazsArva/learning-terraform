resource "azurerm_container_registry" "acr" {
  name                = "exampleapp"
  resource_group_name = "LearningTerraform"
  location            = local.location
  sku                 = "standard"

  tags = {
    "environment": "common"
    "terraform": "true"
  }
}

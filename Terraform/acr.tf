resource "azurerm_container_registry" "acr" {
  name = "exampleapp"
  resource_group_name = "LearningTerraform"
  location = "North Europe"
  sku = "standard"

  tags = {
    "environment": "development"
    "terraform": "true"
  }
}

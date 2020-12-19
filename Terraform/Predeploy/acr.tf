resource "azurerm_container_registry" "acr" {
  name                = "exampleapp"
  resource_group_name = local.resource_group_name
  location            = local.location
  sku                 = "standard"

  tags = {
    "environment": "common"
    "terraform": "true"
  }
}

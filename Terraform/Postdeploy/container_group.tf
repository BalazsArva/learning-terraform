data "azurerm_container_registry" "acr" {
  name = "exampleapp"
  resource_group_name = "LearningTerraform"
}

resource "azurerm_container_group" "app" {
  name                = "learning-terraform-api"
  location            = "North Europe"
  resource_group_name = "LearningTerraform"
  ip_address_type     = "public"
  dns_name_label      = "learning-terraform-api"
  os_type             = "Linux"

  container {
    name   = "learning-terraform-api"
    image  = "${data.azurerm_container_registry.acr.login_server}/learning-terraform-api"
    cpu    = "0.5"
    memory = "1.5"

    ports {
      port     = 443
      protocol = "TCP"
    }
  }

  tags = {
    "environment": "test"
    "terraform": "true"
  }
}

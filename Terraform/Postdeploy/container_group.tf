data "azurerm_container_registry" "acr" {
  name                = "exampleapp"
  resource_group_name = "LearningTerraform"
}
resource "azurerm_container_group" "app" {
  name                = local.api_container_group_name
  location            = local.location
  dns_name_label      = local.api_container_group_name
  resource_group_name = "LearningTerraform"
  ip_address_type     = "public"
  os_type             = "Linux"

  container {
    name   = local.api_container_name
    image  = "${data.azurerm_container_registry.acr.login_server}/${local.api_image_name}:${var.image_tag}"
    cpu    = "0.5"
    memory = "1.5"

    ports {
      port     = 443
      protocol = "TCP"
    }

    ports {
      port     = 80
      protocol = "TCP"
    }

    environment_variables = {
      "ASPNETCORE_URLS" = "https://+:443;http://+:80"
    }
  }

  image_registry_credential {
    server   = data.azurerm_container_registry.acr.login_server
    username = var.image_registry_username
    password = var.image_registry_password
  }

  tags = {
    "environment": "test"
    "terraform": "true"
  }
}

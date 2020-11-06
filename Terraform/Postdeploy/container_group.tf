data "azurerm_container_registry" "acr" {
  name = "exampleapp"
  resource_group_name = "LearningTerraform"
}

variable "image_tag" {
  type = string
}

variable "image_registry_username" {
  type = string
}

variable "image_registry_password" {
  type = string
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
    image  = "learning-terraform-api:${var.image_tag}"
    cpu    = "0.5"
    memory = "1.5"

    ports {
      port     = 443
      protocol = "TCP"
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

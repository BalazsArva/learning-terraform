variable "image_tag" {
  type = string

  validation {
    condition = length(var.image_tag) > 0
    error_message = "Image Tag is mandatory."
  }
}

variable "image_registry_username" {
  type = string

  validation {
    condition = length(var.image_registry_username) > 0
    error_message = "Image Registry username is mandatory."
  }
}

variable "image_registry_password" {
  type = string

  validation {
    condition = length(var.image_registry_password) > 0
    error_message = "Image Registry password is mandatory."
  }
}

variable "db_connection_string" {
  type = string

  validation {
    condition = length(var.db_connection_string) > 0
    error_message = "Database connection string is mandatory."
  }
}

locals {
  location                  = "North Europe"

  api_container_group_name  = "learning-terraform-api"
  api_container_name        = "learning-terraform-api"
  api_image_name            = "learning-terraform-api"

  database_server_name      = "learning-terraform-db"
  database_name             = "learning-terraform-db"
}

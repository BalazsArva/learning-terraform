terraform {
  backend "azurerm" {
    resource_group_name  = local.resource_group_name
    storage_account_name = "learningterraform200910"
    container_name       = "terraformstate"
    key                  = "common.terraform.tfstate"
  }
}

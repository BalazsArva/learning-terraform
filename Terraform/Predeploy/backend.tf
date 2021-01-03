terraform {
  backend "azurerm" {
    resource_group_name  = "LearningTerraform"
    storage_account_name = "learningterraform200910"
    container_name       = "terraformstate"
    key                  = "terraform.predeploy.tfstate"
  }
}

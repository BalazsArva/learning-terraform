provider "azurerm" {
  # Whilst version is optional, we /strongly recommend/ using it to pin the version of the Provider being used
  version = "=2.20.0"

  subscription_id = "081f3987-524b-4242-88fc-6c46c7de6c52"
  client_id       = "d82301b4-8027-4b1e-a588-e6b81b2dd4dc"
  tenant_id       = "b41b72d0-4e9f-4c26-8a69-f949f367c91d"

  features {}
}

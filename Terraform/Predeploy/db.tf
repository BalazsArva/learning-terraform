resource "azurerm_sql_server" "sql_server" {
  name                         = local.database_server_name
  resource_group_name          = "LearningTerraform"
  location                     = local.location
  version                      = "12.0"
  administrator_login          = var.db_server_admin_login
  administrator_login_password = var.db_server_admin_password

  tags = {
    "environment": "test"
    "terraform": "true"
  }
}

resource "azurerm_sql_database" "sql_db" {
  name = local.database_name
  resource_group_name = "LearningTerraform"
  create_mode = "Default"
  edition = "Free"
  location = local.location
  server_name = azurerm_sql_server.sql_server.name

  tags = {
    "environment": "test"
    "terraform": "true"
  }
}

output "sql_server_fqdn" {
  value = azurerm_sql_server.sql_server.fully_qualified_domain_name
}

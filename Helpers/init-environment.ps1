function SetEnvironmentVariables {
    $ClientSecret = [Environment]::GetEnvironmentVariable("LearningTerraform.Client.Secret", [EnvironmentVariableTarget]::User)

    $env:ARM_CLIENT_ID       = "d82301b4-8027-4b1e-a588-e6b81b2dd4dc"
    $env:ARM_CLIENT_SECRET   = $ClientSecret
    $env:ARM_SUBSCRIPTION_ID = "081f3987-524b-4242-88fc-6c46c7de6c52"
    $env:ARM_TENANT_ID       = "b41b72d0-4e9f-4c26-8a69-f949f367c91d"

    Write-Host -ForegroundColor Green "Environment variables successfully configured."
}

SetEnvironmentVariables

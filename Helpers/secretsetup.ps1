function SetAzureClientSecret {
    $SetupUrl = "https://portal.azure.com/#blade/Microsoft_AAD_RegisteredApps/ApplicationMenuBlade/Credentials/appId/d82301b4-8027-4b1e-a588-e6b81b2dd4dc/isMSAApp/"

    do {
        Write-Host -ForegroundColor Yellow "Please enter your Azure Client secret for the App Registration."
        Write-Host -ForegroundColor Yellow "Leave it empty and hit Enter to be taken to the page where you can create one."

        $ClientSecret = Read-Host "> "

        if ("" -eq $ClientSecret -or $null -eq $ClientSecret) {
            Write-Host
            Write-Host -ForegroundColor Yellow (
                "Taking you to the Client Secret creation page. Hit 'New client secret' to create one.")
            
            Write-Host -ForegroundColor Yellow "When you are done, enter it here."
            Write-Host
            Write-Host -ForegroundColor Yellow (
                "Remember that these secrets expire after a configured amount of time after which they can no longer be used.")

            Write-Host

            Invoke-Expression -Command "cmd.exe /C start $SetupUrl"
        }
    }
    while ("" -eq $ClientSecret -or $null -eq $ClientSecret)

    [Environment]::SetEnvironmentVariable("LearningTerraform.Client.Secret", $ClientSecret, [EnvironmentVariableTarget]::User)

    $env:ARM_CLIENT_SECRET   = $ClientSecret

    Clear-Host

    Write-Host -ForegroundColor Green "Client secret successfully configured"
}

SetAzureClientSecret

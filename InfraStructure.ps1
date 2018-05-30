# Log into Azure account.
Login-AzureRMAccount

# Set the values for the resource names.
$location = "West US"
$resourceGroup = "cloud-shell-storage-westus"
$iotHubConsumerGroup = "ServerlessDemoConsumers"
$containerName = "shoppingresults"
$iotDeviceName = "swatantra.yadav@skykick.com"

# These resource names must be globally unique.
# You might need to change these if they are already in use by someone else.
$iotHubName = "serverlessiot"
$storageAccountName = "serverlessresultsstorage"
$serviceBusNamespace = "ServerlessSBNamespace"
$serviceBusQueueName  = "ServerlessSBQueue"

# Create the resource group to be used  
#   for all resources for this tutorial.
#New-AzureRmResourceGroup -Name $resourceGroup -Location $location

# Create the IoT hub.
/*New-AzureRmIotHub -ResourceGroupName $resourceGroup `
    -Name $iotHubName `
    -SkuName "S1" `
    -Location $location `
    -Units 1
    */

# Add a consumer group to the IoT hub for the 'events' endpoint.
Add-AzureRmIotHubEventHubConsumerGroup -ResourceGroupName $resourceGroup `
  -Name $iotHubName `
  -EventHubConsumerGroupName $iotHubConsumerGroup `
  -EventHubEndpointName "events"

# Create the storage account to be used as a routing destination.
# Save the context for the storage account 
#   to be used when creating a container.
$storageAccount = New-AzureRmStorageAccount -ResourceGroupName $resourceGroup `
    -Name $storageAccountName `
    -Location $location `
    -SkuName Standard_LRS `
    -Kind Storage
$storageContext = $storageAccount.Context 

# Create the container in the storage account.
New-AzureStorageContainer -Name $containerName `
    -Context $storageContext

# Create the Service Bus namespace.
New-AzureRmServiceBusNamespace -ResourceGroupName $resourceGroup `
    -Location $location `
    -Name $serviceBusNamespace 

# Create the Service Bus queue to be used as a routing destination.
New-AzureRmServiceBusQueue -ResourceGroupName $resourceGroup `
    -Namespace $serviceBusNamespace `
    -Name $serviceBusQueueName
---
layout: default
title: Cleanup
nav_order: 5
---

## Cleaning up Resources in Azure

### Deleting Cosmos DB

When testing in Azure, always remember to delete the resources once you're done as they might keep generating costs and draining your account for credits.

### Deleting Function app

To delete your Function app you navigate to [Azure Portal](https://portal.azure.com), search for resource groups and click on the resourcegroup you want to delete.

<img width="960" alt="NavigateToResourceGroups" src="https://user-images.githubusercontent.com/39302088/191031430-a5363861-2759-4051-af01-9de8c4b5b74d.png">


After that you click on the "Delete resource group" button.

<img width="960" alt="DeleteResourceGroup" src="https://user-images.githubusercontent.com/39302088/191030080-b658d8e3-cfc7-4b88-a34c-f3d506bc5e52.png">


Finally, you have to confirm that you want to delete the resourcegroup. This is done by writing the name of the resourcegroup you desire to delete and then press the "Delete" button.

<img width="441" alt="ConfirmDelete" src="https://user-images.githubusercontent.com/39302088/191031849-26640fcd-0d08-40d1-9050-b83dace2a0ea.png">

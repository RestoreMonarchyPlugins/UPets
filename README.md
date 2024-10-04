# UPets
Ever wanted a pet? In unturned? Following you around? Here you got one!

## Features
- Supports both MySQL and local JSON data file writing
- Pet will follow the player everywhere
- If `MaxDistance` is reached animal will be teleported to player
- All database calls are made asynchronously
- If you want players to be able to buy pets, Uconomy must be installed.

## Credits
* **AdamAdam** for originally creating the plugin

## Commands
- **/pet help** - Displays help information
- **/pet list** - Displays a list of your pets
- **/pet buy \<name\>** - Buys a pet with specified name
- **/pet shop** - Displays a list of available pets in the shop
- **/pet \<name\>** - Spawns/Despawns a specified pet

## Permissions
```xml
<!-- Primary permission to use pet commands like pet help, pet buy etc. -->
<Permission Cooldown="0">pet</Permission>

<!-- Permissions to own a pet without having to buy them -->
<Permission Cooldown="0">pet.own.cow</Permission>
<Permission Cooldown="0">pet.own.bear</Permission>
<Permission Cooldown="0">pet.own.wolf</Permission>
<Permission Cooldown="0">pet.own.reindeer</Permission>
<Permission Cooldown="0">pet.own.pig</Permission>
<Permission Cooldown="0">pet.own.deer</Permission>
```


## Configuration
```xml
<?xml version="1.0" encoding="utf-8"?>
<PetsConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MessageColor>#FF00FF</MessageColor>
  <MinDistance>5</MinDistance>
  <MaxDistance>50</MaxDistance>
  <UseMySQL>false</UseMySQL>
  <DatabaseAddress>localhost</DatabaseAddress>
  <DatabaseUsername>unturned</DatabaseUsername>
  <DatabasePassword>password</DatabasePassword>
  <DatabaseName>unturned</DatabaseName>
  <DatabaseTableName>PlayersPets</DatabaseTableName>
  <DatabasePort>3306</DatabasePort>
  <EnableOwnerKill>true</EnableOwnerKill>
  <Pets>
    <PetConfig Id="6" Name="cow" Cost="100" Permission="" />
    <PetConfig Id="5" Name="bear" Cost="250" Permission="" />
    <PetConfig Id="3" Name="wolf" Cost="150" Permission="pet.wolf" />
    <PetConfig Id="7" Name="reindeer" Cost="500" Permission="pet.reindeer" />
    <PetConfig Id="4" Name="pig" Cost="150" Permission="pet.pig" />
    <PetConfig Id="1" Name="deer" Cost="150" Permission="pet.deer" />
  </Pets>
</PetsConfiguration>
```

## Translations
```xml
<?xml version="1.0" encoding="utf-8"?>
<Translations xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Translation Id="PetHelpLine1" Value="/pet list - Displays a list of your pets" />
  <Translation Id="PetHelpLine2" Value="/pet buy &lt;name&gt; - Buys a pet with specified name" />
  <Translation Id="PetHelpLine3" Value="/pet shop - Displays a list of available pets in the shop" />
  <Translation Id="PetHelpLine4" Value="/pet &lt;name&gt; - Spawns/Despawns a specified pet" />
  <Translation Id="PetShopAvailable" Value="Available pets:" />
  <Translation Id="PetShopNoPets" Value="There isn't any pet available in the shop" />
  <Translation Id="PetList" Value="Your Pets: {0}" />
  <Translation Id="PetListNone" Value="You don't have any pets" />
  <Translation Id="PetNameRequired" Value="You have to specify pet name" />
  <Translation Id="PetNotFound" Value="Failed to find any pet called {0}" />
  <Translation Id="PetSpawnSuccess" Value="Successfully spawned {0}!" />
  <Translation Id="PetSpawnFail" Value="You don't have {0}" />
  <Translation Id="PetDespawnSuccess" Value="Successfully despawned your {0}!" />
  <Translation Id="PetCantAfford" Value="You can't afford to buy {0} for ${1}" />
  <Translation Id="PetBuySuccess" Value="You successfully bought {0} for ${1}!" />
  <Translation Id="PetBuyAlreadyHave" Value="You already have {0}!" />
  <Translation Id="PetBuyNoPermission" Value="You don't have permission to buy {0}!" />
  <Translation Id="PetKilledByOwner" Value="You killed your pet {0}!" />
</Translations>
```
/*
 *  Vroom Controller BluetoothLE Module
 *  vroomControllerBluetoothLE
 *
 *  Copyright (c) 2017. WonderLeague Corporation.
 */

#import "VroomBluetoothLE.h"

#ifdef __cplusplus
extern "C" {
#endif

    VroomBluetoothLE *_vroomBluetoothLE = nil;
    char *_unityObjectString;
    char *_unityCallbackString;

    void vroomShowAlertDialog (const char *title, const char *message) {
        NSString *titleString = [NSString stringWithCString:title encoding:NSUTF8StringEncoding];
        NSString *messageString = [NSString stringWithCString:message encoding:NSUTF8StringEncoding];
        [VroomBluetoothLE showAlertDialog:titleString message:messageString];
    }
    
    void vroomBluetoothLELogString (NSString *message) {

        NSLog (@"%@", message);
    }

    void vroomBluetoothLELog (char *message) {

        vroomBluetoothLELogString ([NSString stringWithFormat:@"%s", message]);
    }

    // Initialize
	void vroomBluetoothLEInitialize (const char *unityObject, const char *unityCallback) {

        _unityObjectString   = (char *)unityObject;
        _unityCallbackString = (char *)unityCallback;
        _vroomBluetoothLE    = [VroomBluetoothLE new];
        NSString *unityObjectString   = [NSString stringWithCString:unityObject encoding:NSUTF8StringEncoding];
        NSString *unityCallbackString = [NSString stringWithCString:unityCallback encoding:NSUTF8StringEncoding];
		[_vroomBluetoothLE Initialize:unityObjectString unityCallback:unityCallbackString];
	}

    // Destroy
	void vroomBluetoothLEDeinitialize () {

		if (_vroomBluetoothLE != nil) {
			[_vroomBluetoothLE deInitialize];
			_vroomBluetoothLE = nil;

			UnitySendMessage (_unityObjectString, _unityCallbackString, "DeInitialized");
            _unityObjectString = nil;
            _unityCallbackString = nil;
        }
	}

    void vroomBluetoothLEStartScan (bool clearPeripheralList) {

        if (_vroomBluetoothLE != nil) {
            [_vroomBluetoothLE scanForPeripheralsWithServices:clearPeripheralList];
        }
    }
    void vroomBluetoothLEStopScan () {

        if (_vroomBluetoothLE != nil)
            [_vroomBluetoothLE stopScan];
    }

    void vroomBluetoothLERetrieveListOfPeripheralsWithServices (const char *serviceUUIDString) {

        if (_vroomBluetoothLE != nil) {
            NSMutableArray *serviceUUIDs = nil;
          
            if (serviceUUIDString != NULL) {
                NSString *sUUID = [NSString stringWithCString:serviceUUIDString encoding:NSUTF8StringEncoding];
                serviceUUIDs = [[NSMutableArray alloc] init];
                [serviceUUIDs addObject:[CBUUID UUIDWithString:sUUID]];
            }
            
            [_vroomBluetoothLE retrieveListOfPeripheralsWithServices:serviceUUIDs];
        }
    }

    void vroomBluetoothLEConnectToPeripheral (const char *name) {

        if (_vroomBluetoothLE && name != NULL) {
            NSString *nameStr = [NSString stringWithCString:name encoding:NSUTF8StringEncoding];
            [_vroomBluetoothLE connectToPeripheral:nameStr];
        }
    }

    void vroomBluetoothLEDisconnectAll () {
        
        if (_vroomBluetoothLE != nil)
            [_vroomBluetoothLE disconnectAll];
    }

    void vroomBluetoothLEDisconnectPeripheral (const char *name) {

        if (_vroomBluetoothLE && name != NULL) {
            NSString *nameStr = [NSString stringWithCString:name encoding:NSUTF8StringEncoding];
            [_vroomBluetoothLE disconnectPeripheral:nameStr];
        }
    }

    void vroomBluetoothLEGetBatteryService(const char *name) {
        
        if (_vroomBluetoothLE && name != NULL) {
            NSString *nameStr = [NSString stringWithCString:name encoding:NSUTF8StringEncoding];
            [_vroomBluetoothLE readCharacteristic:nameStr service:@"180F" characteristic:@"2A19"];
        }
    }
    
    void vroomBluetoothLEGetDeviceInformationService(const char *name) {
        
        if (_vroomBluetoothLE && name != NULL) {
            NSString *nameStr = [NSString stringWithCString:name encoding:NSUTF8StringEncoding];
            [_vroomBluetoothLE readCharacteristic:nameStr service:@"180A" characteristic:@"2A29"];
            [_vroomBluetoothLE readCharacteristic:nameStr service:@"180A" characteristic:@"2A24"];
            [_vroomBluetoothLE readCharacteristic:nameStr service:@"180A" characteristic:@"2A25"];
            [_vroomBluetoothLE readCharacteristic:nameStr service:@"180A" characteristic:@"2A27"];
            [_vroomBluetoothLE readCharacteristic:nameStr service:@"180A" characteristic:@"2A28"];
        }
    }

    void vroomBluetoothLESubscribeCharacteristic (const char *name) {
        
        if (_vroomBluetoothLE && name != NULL) {
            NSString *nameStr = [NSString stringWithCString:name encoding:NSUTF8StringEncoding];
            [_vroomBluetoothLE subscribeCharacteristic:nameStr service:@"180F" characteristic:@"2A19"];
            [_vroomBluetoothLE subscribeCharacteristic:nameStr service:@"500C0001-164A-4D7A-A6CC-57301B115071" characteristic:@"500C0002-164A-4D7A-A6CC-57301B115071"];
        }
    }
   
    void vroomBluetoothLEUnsubscribeCharacteristic (const char *name) {

        if (_vroomBluetoothLE && name != NULL) {
            NSString *nameStr = [NSString stringWithCString:name encoding:NSUTF8StringEncoding];
            [_vroomBluetoothLE unsubscribeCharacteristic:nameStr service:@"500C0001-164A-4D7A-A6CC-57301B115071" characteristic:@"500C0002-164A-4D7A-A6CC-57301B115071"];
            [_vroomBluetoothLE unsubscribeCharacteristic:nameStr service:@"180F" characteristic:@"2A19"];
        }
    }

    void vroomBluetoothLEGetVroomControllerStatus (const char *name) {
        
        if (_vroomBluetoothLE && name != NULL) {
            NSString *nameStr = [NSString stringWithCString:name encoding:NSUTF8StringEncoding];
            [_vroomBluetoothLE readCharacteristic:nameStr service:@"500C0001-164A-4D7A-A6CC-57301B115071" characteristic:@"500C0003-164A-4D7A-A6CC-57301B115071"];
        }
    }

    int vroomBluetoothLEGetNotificationValue (const char *name, unsigned char **value, int *size) {

        NSInteger length = 0;
        if (_vroomBluetoothLE && name != NULL) {
            NSString *nameStr = [NSString stringWithCString:name encoding:NSUTF8StringEncoding];
            NSData *data = [_vroomBluetoothLE getNotificationValue:nameStr];
            length = data.length;
            if (length > 0) {
                unsigned char *retVal = (unsigned char *)malloc((int)length);
                memcpy(retVal, data.bytes, length);
                *value = retVal;
                *size = (int)length;
            }
        }
        return (int)length;
    }
    
    /*
    void vroomBluetoothLEWriteCharacteristic (char *name, char *service, char *characteristic, unsigned char *data, int length, BOOL withResponse) {
     
        if (_vroomBluetoothLE && name != nil && service != nil && characteristic != nil && data != nil && length > 0)
            NSString *nameStr = [NSString stringWithCString:name encoding:NSUTF8StringEncoding];
            NSString *serviceStr = [NSString stringWithCString:service encoding:NSUTF8StringEncoding];
            NSString *characteristicStr = [NSString stringWithCString:service encoding:NSUTF8StringEncoding];
            [_vroomBluetoothLE writeCharacteristic:nameStr service:serviceStr characteristic:characteristicStr data:[NSData dataWithBytes:data length:length] withResponse:withResponse];
    }
    */

#ifdef __cplusplus
}
#endif

static NSString *const kVroomControllerDeviceName            = @"Vroom";
static NSString *const kVroomControllerDeviceFullName        = @"Vroom Controller";

@implementation VroomBluetoothLE

#pragma mark -
#pragma VroomBluetoothLE

@synthesize _peripherals;
@synthesize _peripheralStates;
@synthesize _unityObject;
@synthesize _unityCallback;
@synthesize bleQueue;
@synthesize initialized;

- (id)init
{
    self = [super init];
    if (self) {
        self._peripherals = [[NSMutableDictionary alloc] init];
        self._peripheralStates = [[NSMutableDictionary alloc] init];
        self.initialized = false;    
    }
    return self;
}

- (void)Initialize:(NSString *)objectNameString unityCallback:(NSString *)unityCallback
{
    _unityObject   = objectNameString;
    _unityCallback = unityCallback;
    bleQueue = dispatch_queue_create("jp.co.wonderleague.vroom.BLE.queue", DISPATCH_QUEUE_SERIAL);
    NSDictionary *options = @{CBCentralManagerOptionShowPowerAlertKey: @YES};
    _centralManager = [[CBCentralManager alloc] initWithDelegate:self queue:bleQueue options:options];
}

- (void)deInitialize
{
    if (_centralManager != nil)
        [self stopScan];

    [_peripherals removeAllObjects];
    [_peripheralStates removeAllObjects];
}

- (void)scanForPeripheralsWithServices:(BOOL)clearPeripheralList
{
    if (_centralManager != nil) {
        if (clearPeripheralList && (_peripherals.count > 0)) {
            [_peripherals removeAllObjects];
            [_peripheralStates removeAllObjects];
        }
        
        NSString *serviceUUID = @"500C0001-164A-4D7A-A6CC-57301B115071";
        NSMutableArray *serviceUUIDs = [[NSMutableArray alloc] init];
        [serviceUUIDs addObject:[CBUUID UUIDWithString:serviceUUID]];
        NSInteger count = [self retrieveListOfPeripheralsWithServices:serviceUUIDs];
        //if (count == 0) {
            if ([_centralManager isScanning] == false) {
                [_centralManager scanForPeripheralsWithServices:serviceUUIDs options:@{CBCentralManagerScanOptionAllowDuplicatesKey: @NO}];
                //[_centralManager scanForPeripheralsWithServices:nil options:@{CBCentralManagerScanOptionAllowDuplicatesKey: @YES}];
            }
        //}
    }
}

- (void)stopScan
{
    if (_centralManager != nil)
        [_centralManager stopScan];
}

- (NSInteger)retrieveListOfPeripheralsWithServices:(NSArray *)serviceUUIDs
{
    NSInteger count = 0;
    if (_centralManager != nil) {
        if (_peripherals != nil) {
            [_peripherals removeAllObjects];
            [_peripheralStates removeAllObjects];
        }

        NSArray * list = [_centralManager retrieveConnectedPeripheralsWithServices:serviceUUIDs];
        if (list != nil) {
            for (int i = 0; i < list.count; ++i) {
                CBPeripheral *peripheral = [list objectAtIndex:i];
                if (peripheral != nil) {
                    NSString *identifier = [[peripheral identifier] UUIDString];
                    NSString *name = [peripheral name];
                    if ([name isEqual:kVroomControllerDeviceFullName]) {
                        [_peripherals setObject:peripheral forKey:identifier];
                        PeripheralState *state = [[PeripheralState alloc]init];
                        [_peripheralStates setObject:state forKey:identifier];
                        NSString *message = [NSString stringWithFormat:@"DiscoveredPeripheral~%@~%@", identifier, name];
                        UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
                        count++;
                    }
                }
            }
        }
    }
    return count;
}

- (void)connectToPeripheral:(NSString *)name
{
    if (_peripherals != nil && name != nil) {
        CBPeripheral *peripheral = [_peripherals objectForKey:name];
        if (peripheral != nil) {
            [_centralManager connectPeripheral:peripheral options:@{CBConnectPeripheralOptionNotifyOnConnectionKey:@YES}];
            //[_centralManager connectPeripheral:peripheral options:@{CBConnectPeripheralOptionNotifyOnDisconnectionKey:@YES}];
        }
    }
}

- (void)disconnectAll
{
    if (_peripherals != nil && [_peripherals count] > 0) {
        NSArray* keys = [_peripherals allKeys];
        for(NSString* key in keys) {
            CBPeripheral *peripheral = [_peripherals objectForKey:key];
            if (peripheral != nil)
                [_centralManager cancelPeripheralConnection:peripheral];
        }
    }
}

- (void)disconnectPeripheral:(NSString *)name
{
    if (_peripherals != nil && name != nil) {
        CBPeripheral *peripheral = [_peripherals objectForKey:name];
        if (peripheral != nil)
            [_centralManager cancelPeripheralConnection:peripheral];
    }
}

- (CBCharacteristic *)getCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString
{
    CBCharacteristic *returnCharacteristic = nil;

    if (name != nil && serviceString != nil && characteristicString != nil && _peripherals != nil) {
        CBPeripheral *peripheral = [_peripherals objectForKey:name];
        if (peripheral != nil) {
            CBUUID *serviceUUID = [CBUUID UUIDWithString:serviceString];
            CBUUID *characteristicUUID = [CBUUID UUIDWithString:characteristicString];

            for (CBService *service in peripheral.services) {
                if ([service.UUID isEqual:serviceUUID]) {
                    for (CBCharacteristic *characteristic in service.characteristics) {
                        if ([characteristic.UUID isEqual:characteristicUUID]) {
                            returnCharacteristic = characteristic;
                        }
                    }
                }
            }
        }
    }

    return returnCharacteristic;
}

- (void)readCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString
{
    if (name != nil && serviceString != nil && characteristicString != nil && _peripherals != nil) {
        CBPeripheral *peripheral = [_peripherals objectForKey:name];
        if (peripheral != nil) {
            CBCharacteristic *characteristic = [_vroomBluetoothLE getCharacteristic:name service:serviceString characteristic:characteristicString];
            if (characteristic != nil)
                [peripheral readValueForCharacteristic:characteristic];
        }
    }
}

- (void)writeCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString data:(NSData *)data withResponse:(BOOL)withResponse
{
    if (name != nil && serviceString != nil && characteristicString != nil && _peripherals != nil && data != nil) {
        CBPeripheral *peripheral = [_peripherals objectForKey:name];
        if (peripheral != nil) {
            CBCharacteristic *characteristic = [_vroomBluetoothLE getCharacteristic:name service:serviceString characteristic:characteristicString];
            if (characteristic != nil) {
                CBCharacteristicWriteType type = CBCharacteristicWriteWithoutResponse;
                
                if (withResponse)
                    type = CBCharacteristicWriteWithResponse;
                
                [peripheral writeValue:data forCharacteristic:characteristic type:type];
            }
        }
    }
}

- (void)subscribeCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString
{
    if (name != nil && _peripherals != nil) {
        CBPeripheral *peripheral = [_peripherals objectForKey:name];
        if (peripheral != nil) {
            CBCharacteristic *characteristic = [_vroomBluetoothLE getCharacteristic:name service:serviceString characteristic:characteristicString];
            
            if (characteristic != nil) {
                [peripheral setNotifyValue:YES forCharacteristic:characteristic];
                NSString *message = [NSString stringWithFormat:@"DidUpdateNotificationStateForCharacteristic~%@~%@~%@", name, characteristic.UUID, @"Subscribe"];
                UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
            }
        }
    }
}

- (void)unsubscribeCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString
{
    if (name != nil && _peripherals != nil) {
        CBPeripheral *peripheral = [_peripherals objectForKey:name];
        if (peripheral != nil) {
            CBCharacteristic *characteristic = [_vroomBluetoothLE getCharacteristic:name service:serviceString characteristic:characteristicString];
            if (characteristic != nil) {
                [peripheral setNotifyValue:NO forCharacteristic:characteristic];
                NSString *message = [NSString stringWithFormat:@"DidUpdateNotificationStateForCharacteristic~%@~%@~%@", name, characteristic.UUID, @"Unsubscribe"];
                UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
            }
        }
    }
}

- (void)centralManagerDidUpdateState:(CBCentralManager *)central
{
    switch (central.state) {
        case CBManagerStateUnsupported:
        {
            NSLog(@"Central State: Unsupported");
            
            NSString *message = [NSString stringWithFormat:@"Error~Bluetooth LE Not Supported"];
            UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
        } break;

        case CBManagerStateResetting:
        {
            NSLog(@"Central State: Resetting");

            NSString *message = [NSString stringWithFormat:@"Error~Bluetooth LE Resetting"];
            UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);            
        } break;

        case CBManagerStateUnauthorized:
        {
            NSLog(@"Central State: Unauthorized");

            NSString *message = [NSString stringWithFormat:@"Error~Bluetooth LE Not Authorized"];
            UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
        } break;

        case CBManagerStatePoweredOff:
        {
            NSLog(@"Central State: Powered Off");
            if (initialized) {
                NSString *appName = [[[NSBundle mainBundle] infoDictionary] objectForKey:@"CFBundleDisplayName"];
                NSString *message = [NSString stringWithFormat:@"\"%@\"がアクセサリに接続できるようにするには、Bluetoothをオンにしてください。", appName];            
                [VroomBluetoothLE showAlertDialog:message message:@""];
            }            
            NSString *message = [NSString stringWithFormat:@"Error~Bluetooth LE Powered Off"];
            UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
        } break;
        
        case CBManagerStatePoweredOn:
        {
            NSLog(@"Central State: Powered On");
            if (! initialized) {
                initialized = true;
                UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], "Initialized");
            }
        } break;

        case CBManagerStateUnknown:
        {
            NSLog(@"Central State: Unknown");

            NSString *message = [NSString stringWithFormat:@"Error~Bluetooth LE Unknown"];
            UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
        } break;

        default:
        {
        }
    }
}

- (void)centralManager:(CBCentralManager *)central didDiscoverPeripheral:(CBPeripheral *)peripheral advertisementData:(NSDictionary *)advertisementData RSSI:(NSNumber *)RSSI
{
    if (_peripherals != nil && peripheral != nil) {
        NSString *name = [advertisementData objectForKey:CBAdvertisementDataLocalNameKey];
        
        if (name == nil)
            name = peripheral.name;

        if ([name isEqual:kVroomControllerDeviceName]) {
            NSString *identifier = nil;

            NSString *foundPeripheral = [self findPeripheralName:peripheral];
            if (foundPeripheral == nil)
                identifier = [[peripheral identifier] UUIDString];
            else
                identifier = foundPeripheral;
            
            NSString *message = nil;

            if (advertisementData != nil && [advertisementData objectForKey:CBAdvertisementDataManufacturerDataKey] != nil) {
                NSData* bytes = [advertisementData objectForKey:CBAdvertisementDataManufacturerDataKey];
                message = [NSString stringWithFormat:@"DiscoveredPeripheral~%@~%@~%@~%@", identifier, name, RSSI, [bytes base64EncodedStringWithOptions:0]];
            } else if (RSSI != 0) {
                message = [NSString stringWithFormat:@"DiscoveredPeripheral~%@~%@~%@~", identifier, name, RSSI];
            } else {
                message = [NSString stringWithFormat:@"DiscoveredPeripheral~%@~%@", identifier, name];
            }

            if (message != nil)
                UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);

            [_peripherals setObject:peripheral forKey:identifier];
            if (foundPeripheral == nil) {
                PeripheralState *state = [[PeripheralState alloc]init];
                [_peripheralStates setObject:state forKey:identifier];
            }
        }
    }
}

- (void)centralManager:(CBCentralManager *)central didDisconnectPeripheral:(CBPeripheral *)peripheral error:(NSError *)error
{
    if (_peripherals != nil) {
        NSString *foundPeripheral = [self findPeripheralName:peripheral];
        if (foundPeripheral != nil) {
            NSString *message = [NSString stringWithFormat:@"DisconnectedPeripheral~%@", foundPeripheral];
            UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
        }
    }
}

- (void)centralManager:(CBCentralManager *)central didFailToConnectPeripheral:(CBPeripheral *)peripheral error:(NSError *)error
{
    if (error) {
        NSString *message = [NSString stringWithFormat:@"Error~FailedToConnectPeripheral~%@", error.description];
        UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
    }
}

- (void)centralManager:(CBCentralManager *)central didConnectPeripheral:(CBPeripheral *)peripheral
{
    NSString *foundPeripheral = [self findPeripheralName:peripheral];
    if (foundPeripheral != nil) {
        peripheral.delegate = self;
        
        NSString *deviceName = peripheral.name;
        NSString *message = [NSString stringWithFormat:@"ConnectedPeripheral~%@~%@", foundPeripheral, deviceName];
        UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);

        [peripheral discoverServices:nil];
    }
}

- (void)peripheral:(CBPeripheral *)peripheral didDiscoverServices:(NSError *)error
{
    if (error) {
        NSString *message = [NSString stringWithFormat:@"Error~DiscoveredServices~%@", error.description];
        UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
    } else {
        NSString *foundPeripheral = [self findPeripheralName:peripheral];
        if (foundPeripheral != nil) {
            for (CBService *service in peripheral.services) {
                NSString *message = [NSString stringWithFormat:@"DiscoveredService~%@~%@", foundPeripheral, [service UUID]];
                UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);

                [peripheral discoverCharacteristics:nil forService:service];
            }
        }
    }
}

- (void)peripheral:(CBPeripheral *)peripheral didDiscoverCharacteristicsForService:(CBService *)service error:(NSError *)error
{
    if (error) {
        NSString *message = [NSString stringWithFormat:@"Error~DiscoveredCharacteristic~%@", error.description];
        UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
    } else {
        NSString *foundPeripheral = [self findPeripheralName:peripheral];
        if (foundPeripheral != nil) {
            for (CBCharacteristic *characteristic in service.characteristics) {
                NSString *message = [NSString stringWithFormat:@"DiscoveredCharacteristic~%@~%@~%@", foundPeripheral, [service UUID], [characteristic UUID]];
                UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
                
                if ([service.UUID isEqual:[CBUUID UUIDWithString:@"180A"]]) {
                    if(!([characteristic.UUID isEqual:[CBUUID UUIDWithString:@"2A50"]])) {
                        [peripheral readValueForCharacteristic:characteristic];
                    }                    
                }
                
                if ([service.UUID isEqual:[CBUUID UUIDWithString:@"180F"]]) {
                    [peripheral readValueForCharacteristic:characteristic];
                    if ([characteristic.UUID isEqual:[CBUUID UUIDWithString:@"2A19"]]) {
                        [peripheral setNotifyValue:YES forCharacteristic:characteristic];
                        NSString *message = [NSString stringWithFormat:@"DidUpdateNotificationStateForCharacteristic~%@~%@~%@", foundPeripheral, characteristic.UUID, @"Subscribe"];
                        UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
                    }
                }
                
                if ([characteristic.UUID isEqual:[CBUUID UUIDWithString:@"500C0002-164A-4D7A-A6CC-57301B115071"]]) {
                    [peripheral setNotifyValue:YES forCharacteristic:characteristic];
                    NSString *message = [NSString stringWithFormat:@"DidUpdateNotificationStateForCharacteristic~%@~%@~%@", foundPeripheral, characteristic.UUID, @"Subscribe"];
                    UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
                }
            }
        }
    }
}

- (void)peripheral:(CBPeripheral *)peripheral didUpdateValueForCharacteristic:(CBCharacteristic *)characteristic error:(NSError *)error
{           
    if (error) {
        NSString *message = [NSString stringWithFormat:@"Error~didUpdateValueForCharacteristic~%@~%@", characteristic.UUID, error.description];
        UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
    } else {
        NSString *foundPeripheral = [self findPeripheralName:peripheral];
        if (foundPeripheral != nil) {
            if (characteristic.value != nil) {
                if ([characteristic.UUID isEqual:[CBUUID UUIDWithString:@"500C0002-164A-4D7A-A6CC-57301B115071"]]) {
                    [self setNotificationValue:peripheral characteristic:characteristic data:characteristic.value];
                } else {
                    NSString *value = [NSString stringWithFormat:@"%@", [characteristic.value base64EncodedStringWithOptions:0]];
                    NSString *message = [NSString stringWithFormat:@"DidUpdateValueForCharacteristic~%@~%@~%@", foundPeripheral, characteristic.UUID, value];
                    UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
                }
            }
        }
    }
}

- (void)peripheral:(CBPeripheral *)peripheral didWriteValueForCharacteristic:(CBCharacteristic *)characteristic error:(NSError *)error
{
    if (error) {
        NSString *message = [NSString stringWithFormat:@"Error~DidWriteCharacteristic~%@", error.description];
        UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
    } else {
        NSString *message = [NSString stringWithFormat:@"DidWriteCharacteristic~%@", characteristic.UUID];
        UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
    }
}

- (void)peripheral:(CBPeripheral *)peripheral didUpdateNotificationStateForCharacteristic:(CBCharacteristic *)characteristic error:(NSError *)error
{
    if (error) {
        NSString *foundPeripheral = [self findPeripheralName:peripheral];
        if (foundPeripheral != nil) {
            NSString *message = [NSString stringWithFormat:@"Error~DidUpdateNotificationStateForCharacteristic~%@~%@~%@", foundPeripheral, characteristic.UUID, error.description];
            UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
        }
    }
}

- (NSString *) findPeripheralName:(CBPeripheral*)peripheral
{
    NSString *foundPeripheral = nil;

    NSEnumerator *enumerator = [_peripherals keyEnumerator];
    id key;
    while ((key = [enumerator nextObject])) {
        CBPeripheral *tempPeripheral = [_peripherals objectForKey:key];
        if ([tempPeripheral isEqual:peripheral]) {
            foundPeripheral = key;
            break;
        }
    }

    return foundPeripheral;
}

- (void) setNotificationValue:(CBPeripheral *)peripheral characteristic:(CBCharacteristic *)characteristic data:(NSData *)data
{
    NSString *identifier = [[peripheral identifier] UUIDString];
    [[_peripheralStates objectForKey:identifier] setNotifyValue:data];
    
    /*
     * NSString *value = [NSString stringWithFormat:@"%@", [characteristic.value base64EncodedStringWithOptions:0]];
     * NSString *message = [NSString stringWithFormat:@"DidUpdateValueForCharacteristic~%@~%@~%@", identifier, characteristic.UUID, [NSString stringWithFormat:@"%@", value]];
     * UnitySendMessage ([_unityObject UTF8String], [_unityCallback UTF8String], [message UTF8String]);
     */
}

- (NSData *)getNotificationValue:(NSString *)name
{
    CBPeripheral *peripheral = [_peripherals objectForKey:name];

    if (peripheral == nil)
        return [[NSData alloc]init];

    NSString *identifier = [[peripheral identifier] UUIDString];
    NSData *value = [[_peripheralStates objectForKey:identifier] getNotifyValue];
    return value;
}

+ (void)showAlertDialog:(NSString *)title message:(NSString *)message
{
    UIViewController *view = UnityGetGLViewController();
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:title message:message preferredStyle:UIAlertControllerStyleAlert];
    UIAlertAction* defaultAction = [UIAlertAction actionWithTitle:@"OK" style:UIAlertActionStyleDefault handler:^(UIAlertAction * action) {}];
    [alert addAction:defaultAction];
    [view presentViewController:alert animated:YES completion:nil];
}

@end

@implementation PeripheralState
#pragma mark -
#pragma PeripheralState
@synthesize _notifyState;
@synthesize _notifyValue;

- (NSData *)getNotifyValue
{
    NSData *data;
    @synchronized (self) {
        data = _notifyValue;
    }
    return data;
}

- (void)setNotifyValue:(NSData *)data
{
    @synchronized (self) {
        _notifyValue = data;
    }
}

@end

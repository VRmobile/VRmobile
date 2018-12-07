/*
 *  Vroom Controller BluetoothLE Module
 *  VroomBluetoothLE
 *
 *  Copyright (c) 2017. WonderLeague Corporation.
 */

#import "Foundation/Foundation.h"
#import "CoreBluetooth/CoreBluetooth.h"

#ifdef __cplusplus
extern "C" {
#endif
    void vroomShowAlertDialog (const char *title, const char *message);
	void vroomBluetoothLEInitialize(const char *unityObject, const char *unityCallback);
	void vroomBluetoothLEDeinitialize();
	void vroomBluetoothLEStartScan (bool clearPeripheralList);
	void vroomBluetoothLEStopScan ();
	void vroomBluetoothLERetrieveListOfPeripheralsWithServices (const char *serviceUUIDString);
	void vroomBluetoothLEConnectToPeripheral (const char *name);
    void vroomBluetoothLEDisconnectAll ();
	void vroomBluetoothLEDisconnectPeripheral (const char *name);
    void vroomBluetoothLEGetBatteryService(const char *name);
    void vroomBluetoothLEGetDeviceInformationService(const char *name);
    void vroomBluetoothLEGetVroomControllerStatus(const char *name);
    void vroomBluetoothLESubscribeCharacteristic (const char *name);
    void vroomBluetoothLEUnsubscribeCharacteristic (const char *name);
    int vroomBluetoothLEGetNotificationValue(const char *name, unsigned char **value, int *size);
    // void vroomBluetoothLEWriteCharacteristic (char *name, char *service, char *characteristic, unsigned char *data, int length, BOOL withResponse);
#ifdef __cplusplus
}
#endif

@interface VroomBluetoothLE : NSObject <CBCentralManagerDelegate, CBPeripheralDelegate>
{
    CBCentralManager *_centralManager;
    NSMutableDictionary *_peripherals;
    NSMutableDictionary *_peripheralStates;
    NSString *_unityObject;
    NSString *_unityCallback;
}

@property (nonatomic, strong) NSMutableDictionary *_peripherals;
@property (nonatomic, strong) NSMutableDictionary *_peripheralStates;
@property (nonatomic) NSString *_unityObject;
@property (nonatomic) NSString *_unityCallback;
@property (nonatomic, strong) dispatch_queue_t bleQueue;
@property (nonatomic) bool initialized;

- (void)Initialize:(NSString *)objectNameString unityCallback:(NSString *)unityCallback;
- (void)deInitialize;
- (void)scanForPeripheralsWithServices:(BOOL)clearPeripheralList;
- (void)stopScan;
- (NSInteger)retrieveListOfPeripheralsWithServices:(NSArray *)serviceUUIDs;
- (void)connectToPeripheral:(NSString *)name;
- (void)disconnectAll;
- (void)disconnectPeripheral:(NSString *)name;
- (CBCharacteristic *)getCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString;
- (void)readCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString;
- (void)writeCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString data:(NSData *)data withResponse:(BOOL)withResponse;
- (void)subscribeCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString;
- (void)unsubscribeCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString;
- (void)setNotificationValue:(CBPeripheral *)peripheral characteristic:(CBCharacteristic *)characteristic data:(NSData *)data;
- (NSData *)getNotificationValue:(NSString *)name;
+ (void)showAlertDialog:(NSString *)title message:(NSString *)message;

@end

@interface PeripheralState : NSObject
{
    NSData *_notifyValue;
    NSInteger *_notifyState;
}
@property (nonatomic, copy) NSData *_notifyValue;
@property (nonatomic) NSInteger *_notifyState;

- (NSData *)getNotifyValue;
- (void)setNotifyValue:(NSData *)data;
@end

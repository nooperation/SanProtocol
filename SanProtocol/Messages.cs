using System;
using System.Collections.Generic;
using System.Text;

namespace SanBot
{
    public class Messages
    {
        public class Audio
        {
            public const uint LoadSound = 0x412484C4;
            public const uint PlaySound = 0x8FC77316;
            public const uint PlayStream = 0x6A2C4CEF;
            public const uint StopBroadcastingSound = 0x866BF5CF;
            public const uint SetAudioStream = 0x5DCD6123;
            public const uint SetMediaSource = 0xEC3CA8EC;
            public const uint PerformMediaAction = 0x559B7F04;
            public const uint StopSound = 0x1A5C9610;
            public const uint SetLoudness = 0x20EDD0C4;
            public const uint SetPitch = 0x7BB86A5B;
        }

        public class Render
        {
            public const uint LightStateChanged = 0x6951DAEC;
        }

        public class Simulation
        {
            public const uint InitialTimestamp = 0x0D094FEA;
            public const uint Timestamp = 0x1E9B31CE;
            public const uint SetWorldGravityMagnitude = 0x86E6A7F6;
            public const uint ActiveRigidBodyUpdate = 0x864418DA;
            public const uint RigidBodyDeactivated = 0x0D938F45;
            public const uint RigidBodyPropertyChanged = 0x45FAAEBC;
            public const uint RigidBodyDestroyed = 0x3A92215C;
        }

        public class AgentController
        {
            public const uint AgentPlayAnimation = 0x00AC2B81;
            public const uint ExitSit = 0x0B617A9A;
            public const uint ObjectInteractionPromptUpdate = 0x1651CD68;
            public const uint ObjectInteractionCreate = 0xBB086E9B;
            public const uint RequestSitOnObject = 0xE5321C47;
            public const uint SitOnObject = 0x191F08C0;
            public const uint SetAgentFiltersBody = 0x09DD53F6;
            public const uint RequestSetAgentFiltersBody = 0x2B87F09D;
            public const uint SetCharacterUserProperty = 0x31D1EC43;
            public const uint CreateSpeechGraphicsPlayer = 0x158B2580;  /* REMOVED 2020-08-13 */
            public const uint RequestSpawnItem = 0x2C21850D;
            public const uint RequestDeleteLatestSpawn = 0xEB3C4296;
            public const uint RequestDeleteAllSpawns = 0x3EB3EDF7;
            public const uint ControlPoint = 0x2DF35CF3;
            public const uint WarpCharacter = 0x75C0AC6B;
            public const uint RequestWarpCharacter = 0x25C093E0;
            public const uint CharacterControlPointInput = 0xFCA3EF20;
            public const uint CharacterControlPointInputReliable = 0x8FB6F456;
            public const uint CharacterControllerInput = 0x3D490CAB;
            public const uint CharacterControllerInputReliable = 0xA7D6EFD1;
            public const uint RequestAgentPlayAnimation = 0x982B98D8;
            public const uint RequestBehaviorStateUpdate = 0x5489A347;
            public const uint AttachToCharacterNode = 0x85BA6E75;
            public const uint DetachFromCharacterNode = 0x80F90328;
            public const uint RequestDetachFromCharacterNode = 0x67B63AA3;
            public const uint SetCharacterNodePhysics = 0x645C4976;
            public const uint WarpCharacterNode = 0x83F1D7DB;
            public const uint CharacterIKBone = 0xBB382C6B;
            public const uint CharacterIKPose = 0xE945D8B8;
            public const uint CharacterIKBoneDelta = 0x4C3B3B4B;
            public const uint CharacterIKPoseDelta = 0x893A18BE;
            public const uint ObjectInteraction = 0xA25F81AB;
            public const uint ObjectInteractionUpdate = 0x17B7D18A;
            public const uint UserReaction = 0x6F5546CE; /* ADDED 2020-09-10 ? */
        }

        public class GameWorld
        {
            public const uint StaticMeshFlagsChanged = 0xAE522F17;
            public const uint StaticMeshScaleChanged = 0xCA6CCC08;
            public const uint Timestamp = 0xD22C9D73;
            public const uint MoveEntity = 0xEFC20B7F;
            public const uint ChangeMaterialVectorParam = 0x403D5704;
            public const uint ChangeMaterialFloatParam = 0x4F20B073;
            public const uint ChangeMaterial = 0x45C605B8;
            public const uint RiggedMeshFlagsChange = 0x3F020C77;
            public const uint RiggedMeshScaleChanged = 0xEA2934E8;
            public const uint ScriptCameraMessage = 0x60C955C0;
            public const uint ScriptCameraCapture = 0x575D5715; // NEW: 2021-03-25
            public const uint UpdateRuntimeInventorySettings = 0x371D99C1;
        }

        public class RegionRegion
        {
            public const uint DynamicSubscribe = 0x513700E2;
            public const uint DynamicPlayback = 0xE87C89BB;
            public const uint MasterFrameSync = 0x5A4AFA33;
            public const uint AgentControllerMapping = 0xBB5865E8;
        }

        public class WorldState
        {
            public const uint CreateWorld = 0x685B436C;
            public const uint DestroyWorld = 0x20C45982;
            public const uint RigidBodyComponentInitialState = 0x065C105B;
            public const uint AnimationComponentInitialState = 0xDE87FDD8;
            public const uint LoadClusterDefinition = 0xA5C4FB23;
            public const uint ComponentRelativeTransform = 0x941E6445;
            public const uint InitiateCluster = 0x349AD257;
            public const uint CreateClusterViaDefinition = 0x73810D53;
            public const uint DestroyCluster = 0x2926D248;
            public const uint DestroyObject = 0x5749A1CD;
            public const uint DestroySourceIdSpace = 0x1505C6D8;
            public const uint CreateCharacterNode = 0x32DC63D7;
            public const uint CreateAgentController = 0xF555FE2D;
            public const uint DestroyAgentController = 0x16406FB7;
        }

        public class ClientKafka
        {
            public const uint FriendResponseLoaded = 0x0AF50C12;
            public const uint PresenceUpdateFanoutLoaded = 0x5915FBFE;
            public const uint FriendTableLoaded = 0xB4AB87F5;
            public const uint RelationshipTableLoaded = 0x0A7562A7;
            public const uint PrivateChatLoaded = 0x4B73CF2C;
            public const uint PrivateChatStatusLoaded = 0x9BC4EF8A;
            public const uint ScriptRegionConsoleLoaded = 0xD3CAA979;
            public const uint ClientMetric = 0x4AC30FE7;
            public const uint RegionHeartbeatMetric = 0xDCF900A4;
            public const uint RegionEventMetric = 0xBA6DB2FC;
            public const uint SubscribeScriptRegionConsole = 0x3BFA4474;
            public const uint UnsubscribeScriptRegionConsole = 0xD49B04C3;
            public const uint ScriptConsoleLog = 0x00B0E15E;
            public const uint LongLivedNotification = 0x46C5FDF3;
            public const uint LongLivedNotificationDelete = 0x59CF6950;
            public const uint LongLivedNotificationsLoaded = 0x3494608D;
            public const uint ShortLivedNotification = 0xAD589C6F;
            public const uint Login = 0x0C0C9D81;
            public const uint LoginReply = 0xA685E82B;
            public const uint EnterRegion = 0x08445006;
            public const uint LeaveRegion = 0xE4ADC2EB;
            public const uint RegionChat = 0x304D3746;
            public const uint PrivateChat = 0x2DC9B029;
            public const uint PrivateChatStatus = 0x955C35EB;
            public const uint PresenceUpdate = 0x1DB989E8;
            public const uint FriendRequest = 0xA356B3ED;
            public const uint FriendRequestStatus = 0x14FFCD37;
            public const uint FriendResponse = 0xE24EBDD3;
            public const uint FriendResponseStatus = 0x22565685;
            public const uint FriendTable = 0x203CC0A8;
            public const uint RelationshipOperation = 0x650939F7;
            public const uint RelationshipTable = 0x078DCC26;
            public const uint InventoryItemCapabilities = 0xA2190F5D;
            public const uint InventoryItemRevision = 0xE3466906;
            public const uint InventoryItemUpdate = 0xD7C7DC26;
            public const uint InventoryItemDelete = 0xB11C8C84;
            public const uint InventoryLoaded = 0x75BAFB95;
            public const uint FriendRequestLoaded = 0xF5361468;
        }

        public class ClientVoice
        {
            public const uint Login = 0x59AC5555;
            public const uint LoginReply = 0xA6972017;
            public const uint AudioData = 0x5A978A32;
            public const uint SpeechGraphicsData = 0xD9306963;
            public const uint LocalAudioData = 0x0D50D087;
            public const uint LocalAudioStreamState = 0xF2FB6AD0;
            public const uint LocalAudioPosition = 0x1798BA9C;
            public const uint LocalAudioMute = 0x56800096;
            public const uint LocalSetRegionBroadcasted = 0x573EE089;
            public const uint LocalSetMuteAll = 0x90DA7ED3;
            public const uint GroupAudioData = 0x47C4FFDF;
            public const uint LocalTextData = 0xC91B2D1C;
            public const uint MasterInstance = 0x88C28A79;
            public const uint VoiceModerationCommand = 0x3F7171FB;
            public const uint VoiceModerationCommandResponse = 0x742CE528;
            public const uint VoiceNotification = 0x3A168D81;
        }

        public class ClientRegion
        {
            public const uint UserLogin = 0x3902800A;
            public const uint UserLoginReply = 0x30CDBED6;
            public const uint AddUser = 0xF6B9093E;
            public const uint RemoveUser = 0x0A7FC621;
            public const uint RenameUser = 0xC67C58F7;
            public const uint ChatMessageToClient = 0x083642BD;
            public const uint ChatMessageToServer = 0xDDDEC199;
            public const uint SetAgentController = 0xD6F4CF23;
            public const uint DebugTimeChangeToServer = 0x41FE0612;
            public const uint VisualDebuggerCaptureToServer = 0x0741CA9B;
            public const uint ClientStaticReady = 0xF8E77C8E;
            public const uint ClientDynamicReady = 0x575AC239;
            public const uint ClientRegionCommandMessage = 0xECE56EFD;
            public const uint RequestDropPortal = 0x7D22C30C;
            public const uint VibrationPulseToClient = 0x0D3809EB;
            public const uint TeleportTo = 0x5C7CC1FC;
            public const uint TeleportToUri = 0x2BDBDB56;
            public const uint TeleportToEditMode = 0x706F63FB;
            public const uint DebugTimeChangeToClient = 0x5178DC5E;
            public const uint VisualDebuggerCaptureToClient = 0xF66AD9BF;
            public const uint ScriptModalDialog = 0x88023C72;
            public const uint ScriptModalDialogResponse = 0xB34F3A45;
            public const uint TwitchEventSubscription = 0x981AB0D6;
            public const uint TwitchEvent = 0x28F54053;
            public const uint InitialChunkSubscribed = 0xB4E1AB7B;
            public const uint ClientKickNotification = 0x4B68A51C;
            public const uint ClientSmiteNotification = 0x58003034;
            public const uint ClientMuteNotification = 0x6188A537;
            public const uint ClientVoiceBroadcastStartNotification = 0x7E28AEAF;
            public const uint ClientVoiceBroadcastStopNotification = 0xC33DE58B;
            public const uint ClientRuntimeInventoryUpdatedNotification = 0x9643B9C3;
            public const uint ClientSetRegionBroadcasted = 0x87341F77;
            public const uint SubscribeCommand = 0xABDA80C7;
            public const uint UnsubscribeCommand = 0xA36E9F9C;
            public const uint ClientCommand = 0xB87F9C66;
            public const uint OpenStoreListing = 0x05C1A8D7D;
            public const uint OpenUserStore = 0x53078A1E;
            public const uint OpenQuestCharacterDialog = 0x4221836F;
            public const uint UIScriptableBarStart = 0x036164050;
            public const uint UIScriptableBarStopped = 0xBAFD799D;
            public const uint UIScriptableBarCancel = 0x604E18DE;
            public const uint UIHintTextUpdate = 0x64225637;
            public const uint QuestOfferResponse = 0x4DB48E35;
            public const uint QuestCompleted = 0xE1EE5F5D;
            public const uint QuestRemoved = 0x34793AB0;
            public const uint ShowWorldDetail = 0x5F483F0C;
            public const uint ShowTutorialHint = 0x581827CC;
            public const uint TutorialHintsSetEnabled = 0xE4C496DF;
            public const uint ReactionDefinition = 0x1753788; // NEW: 2021-03-25
            public const uint SystemReactionDefinition = 0xFA87F231; // NEW: 2021-03-25
            public const uint UpdateReactions = 0x9B5B20E9; // NEW: 2021-03-25
            public const uint AddReaction = 0x28323E96; // NEW: 2021-03-25
            public const uint RemoveReaction = 0x3F337471; // NEW: 2021-03-25
        }

        public class EditServer
        {
            public const uint UserLogin = 0x046D3C1E;
            public const uint UserLoginReply = 0xE227C3E2;
            public const uint AddUser = 0x50155562;
            public const uint RemoveUser = 0x5729AC25;
            public const uint OpenWorkspace = 0xBC512F47;
            public const uint CloseWorkspace = 0x43C06583;
            public const uint EditWorkspaceCommand = 0x76363E28;
            public const uint SaveWorkspace = 0x7C7BDCA8;
            public const uint SaveWorkspaceReply = 0xFAE838FC;
            public const uint BuildWorkspace = 0x5963934F;
            public const uint UpdateWorkspaceClientBuiltBakeData = 0xF12FD324;
            public const uint BuildWorkspaceCompileReply = 0x15B220E0;
            public const uint BuildWorkspaceProgressUpdate = 0xC9FCDB71;
            public const uint BuildWorkspaceUploadReply = 0xF090AF8E;
            public const uint WorkspaceReadyReply = 0x7D87DBEA;
            public const uint SaveWorkspaceSelectionToInventory = 0x7E37F335;
            public const uint SaveWorkspaceSelectionToInventoryReply = 0x439C3637;
            public const uint InventoryCreateItem = 0xB5487205;
            public const uint InventoryDeleteItem = 0x6F75848E;
            public const uint InventoryChangeItemName = 0x690612C6;
            public const uint InventoryChangeItemState = 0x6C202756;
            public const uint InventoryModifyItemThumbnailAssetId = 0x2C2AE45E;
            public const uint InventoryModifyItemCapabilities = 0xF93582DD;
            public const uint InventorySaveItem = 0xA67D88F8;
            public const uint InventoryUpdateItemReply = 0x144D39F8;
            public const uint InventoryItemUpload = 0xF2E11A50;
            public const uint InventoryItemUploadReply = 0xA25566F4;
            public const uint InventoryCreateListing = 0xBE2C532E;
            public const uint InventoryCreateListingReply = 0x4EA3D072;
            public const uint BeginEditServerSpawn = 0xB5BFECD3;
            public const uint EditServerSpawnReady = 0xB3623297;
        }

        public class AnimationComponent
        {
            public const uint FloatVariable = 0x0B3B7D2E;
            public const uint FloatNodeVariable = 0x4C1B3DF2;
            public const uint FloatRangeNodeVariable = 0x91419DEB;
            public const uint VectorVariable = 0x23314E53;
            public const uint QuaternionVariable = 0x0CC9F1B8;
            public const uint Int8Variable = 0xC11AFDE7;
            public const uint BoolVariable = 0xA67454F0;
            public const uint CharacterTransform = 0xAB2F1EB1;
            public const uint CharacterTransformPersistent = 0x970F93D4;
            public const uint CharacterAnimationDestroyed = 0x53A4BF26;
            public const uint AnimationOverride = 0x8C738C9E;
            public const uint BehaviorInternalState = 0xCE9B5148;
            public const uint CharacterBehaviorInternalState = 0x16C090B1;
            public const uint BehaviorStateUpdate = 0x217192BE;
            public const uint BehaviorInitializationData = 0x7846436E;
            public const uint CharacterSetPosition = 0x51A1705A;
            public const uint PlayAnimation = 0x009385A0;
        }
    }
}

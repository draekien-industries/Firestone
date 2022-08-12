import {
  AssetHoldersClient,
  AssetsClient,
  FireGraphsClient,
  FireTablesClient,
  IAssetHoldersClient,
  IAssetsClient,
  IFireGraphsClient,
  IFireTablesClient,
  ILineItemsClient,
  LineItemsClient,
} from "../apiClients/firestone.generated";

const baseUrl = process.env.NEXT_PUBLIC_FIRESTONE_API_URL;

export interface IFirestoneService {
  tables: () => IFireTablesClient;
  lineItems: () => ILineItemsClient;
  assets: () => IAssetsClient;
  assetHolders: () => IAssetHoldersClient;
  graphs: () => IFireGraphsClient;
}

export const firestoneService: IFirestoneService = {
  tables: () => new FireTablesClient(baseUrl),
  lineItems: () => new LineItemsClient(baseUrl),
  assets: () => new AssetsClient(baseUrl),
  assetHolders: () => new AssetHoldersClient(baseUrl),
  graphs: () => new FireGraphsClient(baseUrl),
};

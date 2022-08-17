import { useEffect, useState } from 'react';
import { FireTableDto } from '../apiClients/firestone.generated';
import { TableCell, TableRow, TableProps } from '../components';
import { firestoneService } from '../services/firestoneService';
import { numberAsCurrency } from '../utils/currency.util';
import { dropDate } from '../utils/datetime.util';
import { difference } from '../utils/math.util';

export interface useFireTableProps {
  tableId?: string;
}

export interface useFireTableResult extends TableProps {
  data: FireTableDto;
}

export const useFireTable = (props: useFireTableProps): useFireTableResult => {
  const { tableId } = props;

  const [tableData, setTableData] = useState<FireTableDto>({});

  useEffect(() => {
    if (!tableId) return;

    firestoneService.tables().get(tableId).then(setTableData);
  }, [tableId]);

  if (!tableData?.lineItems || !tableData.assetHolders) {
    return {} as useFireTableResult;
  }

  const lineItems = tableData.lineItems; // rows

  const rows = lineItems.map((li, index) => {
    const month = dropDate(li.date);

    const assets: TableCell[] =
      tableData.assetHolders?.map((ah) => {
        const assetHolderName = ah.name ?? 'Unknown';

        if (!li.assets?.length) return {} as TableCell;

        const filteredAssets = li.assets.find((i) => i.assetHolderId == ah.id);

        const result: TableCell = {
          [assetHolderName]: numberAsCurrency(filteredAssets?.amount),
        };

        return result;
      }) ?? [];

    const total = numberAsCurrency(li.assetsTotal);
    const delta = numberAsCurrency(
      difference(li.assetsTotal, lineItems.at(index - 1)?.assetsTotal)
    );

    const row = [
      { Month: month },
      ...assets,
      { Total: total },
      { Delta: delta },
    ];

    return row;
  });

  return { title: tableData.name ?? 'FIRE table', rows, data: tableData };
};

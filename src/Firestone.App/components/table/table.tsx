import { FC } from 'react';

export type TableCell = { [key: string]: string };
export type TableRow = TableCell[];

export interface TableProps {
  title: string;
  rows: TableRow[];
}

export const Table: FC<TableProps> = ({ title, rows }) => {
  if (!rows || !rows.length) return null;

  console.log(rows);

  return (
    <table className="table table-auto border-separate border-spacing-y-0.5 w-full rounded-xl overflow-hidden">
      <caption className="text-slate-200 font-semibold text-xl text-left mb-6 ml-4">
        {title}
      </caption>
      <thead className="table-header-group bg-indigo-500/50 font-semibold">
        <tr className="table-row">
          {rows[0].map((cell, index) => (
            <th key={index} className="table-cell text-left py-2 px-4">
              {Object.keys(cell)[0]}
            </th>
          ))}
        </tr>
      </thead>
      <tbody className="table-row-group bg-indigo-500/10">
        {rows.map((cols, index) => (
          <tr key={index} className="table-row">
            {cols.map((cell, index) => (
              <td key={index} className="table-cell py-2 px-4">
                {cell[Object.keys(cell)[0]]}
              </td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
};

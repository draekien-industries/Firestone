import { FC } from 'react';
import {
  AssetHolderDto,
  AssetHolderSummaryDto,
  LineItemDto,
} from '../../apiClients/firestone.generated';
import { dropDate } from '../../utils/datetime.util';

export interface AddRecordFormFieldProps {
  assetHolders: AssetHolderSummaryDto[] | AssetHolderDto[];
  mostRecentLineItem: LineItemDto;
}

export const AddRecordFormFields: FC<AddRecordFormFieldProps> = ({
  assetHolders,
  mostRecentLineItem,
}) => {
  if (!mostRecentLineItem || !mostRecentLineItem.date) return null;

  const lineItemDate = new Date(mostRecentLineItem.date);
  const monthOption1 = new Date(lineItemDate);
  const monthOption2 = new Date(
    lineItemDate.setMonth(lineItemDate.getMonth() + 1)
  );

  return (
    <>
      <div className="mb-2">
        <label htmlFor="asset-holder" className="block text-slate-400 mb-2">
          Asset Holder
        </label>
        <select
          id="asset-holder"
          name="asset-holder"
          className="form-select rounded-md w-full bg-slate-900 text-slate-200 border-transparent focus:border-indigo-700 focus:ring-0">
          {assetHolders.map((ah, index) => (
            <option key={index} value={ah.id}>
              {ah.name}
            </option>
          ))}
        </select>
      </div>
      <div className="mb-2">
        <label htmlFor="month" className="block text-slate-400 mb-2">
          Month
        </label>
        <select
          id="month"
          name="month"
          defaultValue={monthOption2.toString()}
          className="rounded-md w-full bg-slate-900 text-slate-200 border-transparent focus:border-indigo-700 focus:ring-0">
          <option value={monthOption1.toString()}>
            {dropDate(monthOption1)}
          </option>
          <option value={monthOption2.toString()}>
            {dropDate(monthOption2)}
          </option>
        </select>
      </div>
      <div className="mb-2">
        <label htmlFor="amount" className="block text-slate-400 mb-2">
          Amount <span className="text-red-500">*</span>
        </label>
        <div className="relative">
          <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
            <span className="text-slate-200 sm:text-sm"> $ </span>
          </div>
          <input
            id="amount"
            name="amount"
            className="rounded-md w-full pl-7 bg-slate-900 text-slate-200 border-transparent focus:border-indigo-700 focus:ring-0"
            type="text"
            placeholder="0.00"
            required
          />
        </div>
      </div>
    </>
  );
};

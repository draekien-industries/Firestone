import React, { FC, FormEvent } from 'react';
import { XIcon } from '@heroicons/react/solid';

export interface FormModalProps {
  title: string | React.ReactNode;
  show: boolean;
  onClose: () => void;
  onSubmit: (e: FormEvent<HTMLFormElement>) => Promise<void>;
  children: React.ReactNode;
}

export const FormModal: FC<FormModalProps> = ({
  title,
  show,
  onClose,
  onSubmit,
  children,
}) => {
  return show ? (
    <>
      <form
        id="modal"
        onSubmit={async (e) => {
          console.log('submit');
          await onSubmit(e);
          onClose();
        }}
        aria-labelledby="modal-title"
        role="dialog"
        className="flex justify-center items-center overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
        <div className="relative w-auto my-6 mx-auto max-w-6xl">
          <div
            id="modal-content"
            className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-slate-800 outline-none focus:outline-none">
            <div
              id="modal-header"
              className="flex items-start justify-between p-5 border-b border-solid border-slate-700 rounded-t">
              <h3
                id="modal-title"
                className="text-3xl text-slate-200 font-semibold">
                {title}
              </h3>
              <button
                className="p-1 ml-auto bg-transparent border-0 text-slate-200 opacity-50 float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                onClick={onClose}>
                <XIcon className="text-slate-200 h-6 w-6" />
              </button>
            </div>
            <div id="modal-body" className="relative p-6 flex-auto">
              {children}
            </div>
            <div
              id="modal-footer"
              className="flex items-center justify-start flex-row-reverse p-6 border-t borde-solid border-slate-700 rounded-b">
              <button
                className="bg-emerald-600 text-slate-200 hover:bg-emerald-500 font-bold uppercase text-sm px-6 py-1 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 ease-linear transition-all duration-150"
                type="submit">
                Save
              </button>
              <button
                className="text-red-500 hover:text-red-400 background-transparent font-bold uppercase px-6 py-1 text-sm outline-none focus:outline-none mr-1 ease-linear transition-all duration-150"
                type="reset"
                onClick={onClose}>
                Close
              </button>
            </div>
          </div>
        </div>
      </form>
      <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
    </>
  ) : null;
};

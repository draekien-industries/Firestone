import type { NextPage } from "next";
import Head from "next/head";
import { useRouter } from "next/router";
import { useEffect, useMemo, useState } from "react";
import { FireGraphDto, FireTableDto } from "../../apiClients/firestone.generated";
import { firestoneService } from "../../services/firestoneService";
import { Line } from "react-chartjs-2";
import { dropDate } from '../../utils/datetime.util';
import {
  CategoryScale,
  Chart,
  ChartData,
  Legend,
  LinearScale,
  LineElement,
  PointElement,
  ScatterDataPoint,
  Tooltip,
} from 'chart.js';
import { difference } from '../../utils/math.util';
import {
  numberAsCurrency,
  stringOrNumberAsCurrency,
} from '../../utils/currency.util';
import { AddRecordFormFields } from '../../components/add-record-form-fields/addRecordFormFields';
import { FormModal } from '../../components';

Chart.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Tooltip,
  Legend
);

const Table: NextPage = () => {
  const router = useRouter();
  const { tableid } = router.query;

  const [table, setTable] = useState<FireTableDto>();
  const [graph, setGraph] = useState<FireGraphDto>();
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);

  useEffect(() => {
    if (!tableid) return;

    const getTable = async () => {
      const table = await firestoneService.tables().get(tableid as string);
      const graph = await firestoneService.graphs().get(tableid as string);

      setTable(table);
      setGraph(graph);
      setLoading(false);
    };

    getTable();
  }, [tableid]);

  const lineData = useMemo(() => {
    if (!graph) return;

    const recordedAssetsDates = graph.recordedAssets!.map((a) =>
      dropDate(a.date)
    );
    const projectedAssetsDates = graph.projectedAssets!.map((a) =>
      dropDate(a.date)
    );

    const labels = recordedAssetsDates.concat(projectedAssetsDates);
    const uniqueLables = labels.filter(
      (value, index, self) => self.indexOf(value) === index
    );

    const recordedAssets = graph.recordedAssets!.map((ra) => ra.amount ?? null);
    const numberOfRecords = recordedAssets.length;
    const projectedAssets = graph.projectedAssets!.map(
      (pa) => pa.amount ?? null
    );
    const projectedAssetsData = new Array(numberOfRecords)
      .fill(null)
      .concat(projectedAssets);
    const retirementTarget = graph.adjustedTargets!.retirementTargets!.map(
      (rt) => rt.amount ?? null
    );
    const coastTarget = graph.adjustedTargets!.coastTargets!.map(
      (ct) => ct.amount ?? null
    );
    const minimumGrowthTarget =
      graph.adjustedTargets!.minimumGrowthTargets!.map(
        (mgt) => mgt.amount ?? null
      );

    const data: ChartData<
      'line',
      (number | ScatterDataPoint | null)[],
      unknown
    > = {
      labels: uniqueLables,
      datasets: [
        {
          label: 'Recorded Assets',
          data: recordedAssets,
          borderColor: 'rgb(75, 192, 192)',
          pointBorderWidth: 0,
          pointBorderColor: 'rgb(75, 192, 192)',
          tension: 0.1,
        },
        {
          label: 'Projected Assets',
          data: projectedAssetsData,
          borderColor: 'rgb(75, 192, 192)',
          borderDash: [5, 5],
          pointBorderWidth: 0,
          pointBorderColor: 'rgb(75, 192, 192)',
          tension: 0.1,
        },
        {
          label: 'Retirement Target',
          data: retirementTarget,
          borderColor: 'rgb(75, 192, 2)',
          pointBorderWidth: 0,
          pointBorderColor: 'rgb(75, 192, 2)',
          tension: 0.1,
        },
        {
          label: 'Coast Target',
          data: coastTarget,
          borderColor: 'rgb(192, 192, 192)',
          pointBorderWidth: 0,
          pointBorderColor: 'rgb(192, 192, 192)',
          tension: 0.1,
        },
        {
          label: 'Minimum Growth Target',
          data: minimumGrowthTarget,
          borderColor: 'rgb(75, 192, 2)',
          borderDash: [5, 5],
          pointBorderWidth: 0,
          pointBorderColor: 'rgb(75, 192, 2)',
          tension: 0.1,
        },
      ],
    };

    console.log(data);

    return data;
  }, [graph]);

  return (
    <>
      <Head>
        <title>Create Next App</title>
        <meta name="description" content="Generated by create next app" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <div className="container mx-auto">
        <h1 className="text-xl text-slate-200">{table?.name}</h1>
        <section className="my-8">
          {lineData && (
            <details className="container p-4 shadow-xl rounded-lg bg-slate-800 text-slate-300">
              <summary className="text-lg cursor-pointer">
                Graph of {table?.name}
              </summary>
              <div className="m-4">
                <Line
                  data={lineData}
                  options={{
                    responsive: true,
                    color: 'rgb(226, 232, 240)',
                    scales: {
                      y: {
                        ticks: {
                          callback: (val) => stringOrNumberAsCurrency(val),
                          color: 'rgb(226, 232, 240)',
                        },
                        grid: { color: 'rgb(71 85 105)' },
                      },
                      x: {
                        ticks: { color: 'rgb(226, 232, 240)' },
                        grid: { color: 'rgb(71 85 105)' },
                      },
                    },
                    plugins: {
                      legend: { position: 'right' },
                      tooltip: {
                        callbacks: {
                          label: (context) => {
                            var label = context.dataset.label || '';

                            if (label) {
                              label += ': ';
                            }
                            if (context.parsed.y !== null) {
                              label += numberAsCurrency(context.parsed.y);
                            }
                            return label;
                          },
                        },
                      },
                    },
                  }}
                />
              </div>
            </details>
          )}
        </section>
        <section className="my-8">
          <div className="container p-4 shadow-xl rounded-lg bg-slate-800 text-slate-300">
            <table className="table table-auto border-collapse border-spacing-0 w-full">
              <thead className="table-header-group border-b-4 border-b-slate-400 font-semibold">
                <td className="table-cell py-2">Month</td>
                {table?.assetHolders?.map((ah, index) => (
                  <td key={index}>{ah.name}</td>
                ))}
                <td className="table-cell">Total</td>
                <td className="table-cell">Delta</td>
              </thead>
              <tbody className="table-row-group">
                {table?.lineItems?.map((li, index) => (
                  <tr
                    key={index}
                    className="table-row border-b-2 border-b-slate-600">
                    <td className="table-cell py-2">{dropDate(li.date)}</td>
                    {li.assets?.map((a, index) => (
                      <td key={index} className="table-cell">
                        {numberAsCurrency(a.amount)}
                      </td>
                    ))}
                    <td className="table-cell">
                      {numberAsCurrency(li.assetsTotal)}
                    </td>
                    <td className="table-cell">
                      {numberAsCurrency(
                        difference(
                          li.assetsTotal,
                          table?.lineItems?.at(index - 1)?.assetsTotal
                        )
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          <div className="container flex justify-end my-4">
            <button
              className="bg-indigo-500 hover:bg-indigo-400 px-4 py-1 rounded-md shadow-lg shadow-indigo-500/30 hover:shadow-indigo-500/70"
              type="button"
              onClick={() => setShowModal(!showModal)}>
              add
            </button>
          </div>
        </section>
        <FormModal
          title="Add Line Item"
          show={showModal}
          onClose={() => setShowModal(false)}
          onSubmit={async (e) => {
            e.preventDefault();
            const data = new FormData(e.currentTarget);
            console.log(
              data.get('asset-holder'),
              data.get('month'),
              data.get('amount')
            );
          }}>
          <AddRecordFormFields
            assetHolders={table?.assetHolders ?? []}
            mostRecentLineItem={table?.lineItems?.at(-1) ?? {}}
          />
        </FormModal>
      </div>
    </>
  );
};

export default Table;

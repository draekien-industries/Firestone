import type { NextPage } from 'next'
import Head from 'next/head'
import Image from 'next/image'
import styles from "../styles/Home.module.css";
import { useEffect, useState } from "react";
import { firestoneService } from "../services/firestoneService";
import { FireTableSummaryDto } from "../apiClients/firestone.generated";
import Link from "next/link";

const Home: NextPage = () => {
  const [tables, setTables] = useState<FireTableSummaryDto[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getTables = async () => {
      const tables = await firestoneService.tables().list();
      setTables(tables.results ?? []);
      setLoading(false);
    };

    getTables();
  }, []);

  return (
    <>
      <Head>
        <title>Tables | 🔥Firestone</title>
        <meta name="description" content="Tables available to be viewed" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <div className="container mx-auto">
        {loading ? (
          <div>Loading...</div>
        ) : (
          <table>
            <thead>
              <td>Name</td>
              <td>AssetHolders</td>
            </thead>
            <tbody>
              {tables.map((table, index) => (
                <Link key={index} href={{ pathname: "/tables/[tableid]", query: { tableid: table.id } }}>
                  <tr className="cursor-pointer">
                    <td>{table.name}</td>
                    <td>
                      {table.assetHolders?.map((assetHolder, index) => (
                        <span key={index}>{assetHolder.name}</span>
                      ))}
                    </td>
                  </tr>
                </Link>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </>
  );
};

export default Home

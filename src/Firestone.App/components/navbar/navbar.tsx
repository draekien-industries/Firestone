import Link from "next/link";
import { FC } from "react";

export const Navbar: FC = () => {
  return (
    <nav className="flex justify-between px-12 py-8 text-lg text-slate-200 bg-indigo-900 shadow-xl shadow-indigo-900/50">
      <Link href="/">
        <a className="font-semibold">🔥Firestone</a>
      </Link>
    </nav>
  );
};

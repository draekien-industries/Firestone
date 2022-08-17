import React, { FC } from "react";
import { Footer } from "../footer";
import { Navbar } from "../navbar";

export const Layout: FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <div className="min-h-screen w-full flex flex-col">
      <Navbar />
      <main className="px-12 py-12 flex-1">{children}</main>
      <Footer />
    </div>
  );
};

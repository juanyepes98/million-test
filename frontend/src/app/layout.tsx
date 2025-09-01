import type { Metadata } from "next";
import "./globals.css";
import React from "react";
import {AuthProvider} from "@/context/auth-context";
import {MainLayout} from "@/components/layout/main-layout";

export const metadata: Metadata = {
  title: "Million Test",
  description: "Million Test App",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
        <body>
            <AuthProvider>
                <MainLayout>{children}</MainLayout>
            </AuthProvider>
        </body>
    </html>
  );
}

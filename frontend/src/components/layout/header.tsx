"use client";

import { useState } from "react";
import {useAuth} from "@/context/auth-context";
import {Hamburger} from "@/components/ui/hamburguer";
import {useRouter} from "next/navigation";

interface MenuItem {
    label: string;
    href?: string;
}

export const Header = () => {
    const [isOpen, setIsOpen] = useState(false);
    const { isAuthenticated, logout } = useAuth();
    const router = useRouter();

    const handleLogout = () => {
        logout();
        router.push("/");
    };

    const guestMenu: MenuItem[] = [
        { label: "HOME", href: "/" },
        { label: "LOG IN", href: "/login" },
        { label: "SIGN UP", href: "/register" },
    ];

    const authMenu: MenuItem[] = [
        { label: "Dashboard", href: "/dashboard" },
        { label: "Profile", href: "/profile" },
        { label: "Log Out", href: "/logout" },
    ];

    const menu = isAuthenticated ? authMenu : guestMenu;

    const renderMenuItem = (item: MenuItem) => {
        if (item.label === "Log Out") {
            return (
                <button
                    key={item.label}
                    onClick={handleLogout}
                    className="hover:text-gray-300"
                >
                    {item.label}
                </button>
            );
        }

        return (
            <a key={item.label} href={item.href} className="hover:text-gray-300">
                {item.label}
            </a>
        );
    };

    return (
        <header className="bg-gray-800 text-white shadow-md">
            <div className="container mx-auto px-4 flex justify-between items-center h-16">
                <h1 className="text-xl font-bold">MILLION TEST</h1>

                {/* Desktop Menu */}
                <nav className="hidden md:flex gap-6">
                    {menu.map(renderMenuItem)}
                </nav>

                {/* Mobile Hamburger */}
                <div className="md:hidden">
                    <Hamburger isOpen={isOpen} toggle={() => setIsOpen(!isOpen)} />
                </div>
            </div>

            {/* Mobile Menu */}
            {isOpen && (
                <nav className="md:hidden bg-gray-800 px-4 py-3">
                    {menu.map((item) =>
                        item.label === "Log Out" ? (
                            <button
                                key={item.label}
                                onClick={handleLogout}
                                className="block py-2 border-b border-gray-700 last:border-none hover:text-gray-300 w-full text-left"
                            >
                                {item.label}
                            </button>
                        ) : (
                            <a
                                key={item.label}
                                href={item.href}
                                className="block py-2 border-b border-gray-700 last:border-none hover:text-gray-300"
                            >
                                {item.label}
                            </a>
                        )
                    )}
                </nav>
            )}
        </header>
    );
};

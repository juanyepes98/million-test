"use client";

type Props = {
    isOpen: boolean;
    toggle: () => void;
};

export const Hamburger = ({ isOpen, toggle }: Props) => {
    return (
        <button
            onClick={toggle}
            className="flex flex-col justify-center items-center w-8 h-8 focus:outline-none"
        >
      <span
          className={`block h-1 w-6 bg-white transform transition duration-300 ${
              isOpen ? "rotate-45 translate-y-2" : ""
          }`}
      />
            <span
                className={`block h-1 w-6 bg-white my-1 transition duration-300 ${
                    isOpen ? "opacity-0" : ""
                }`}
            />
            <span
                className={`block h-1 w-6 bg-white transform transition duration-300 ${
                    isOpen ? "-rotate-45 -translate-y-2" : ""
                }`}
            />
        </button>
    );
};

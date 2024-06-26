import React from "react";
import { MdKeyboardArrowLeft, MdKeyboardArrowRight } from "react-icons/md";
import { Button } from "../ui/button";

interface PaginationProps {
  pageIndex: number;
  pageCount: number;
  pageSize: number;
  totalRecords: number;
  setPage: (pageIndex: number) => void;
}
const Pagination: React.FC<PaginationProps> = ({
  pageIndex,
  pageCount,
  pageSize,
  setPage,
  totalRecords,
}) => {
  const getPaginationNumbers = () => {
    const pageNumbers: (number | string)[] = [];

    if (pageCount <= 7) {
      for (let i = 1; i <= pageCount; i++) {
        pageNumbers.push(i);
      }
    } else {
      pageNumbers.push(1); // Always include the first page

      if (pageIndex > 3) {
        pageNumbers.push("...");
      }

      for (
        let i = Math.max(2, pageIndex - 1);
        i <= Math.min(pageIndex + 1, pageCount - 1);
        i++
      ) {
        pageNumbers.push(i);
      }

      if (pageIndex < pageCount - 2) {
        pageNumbers.push("...");
      }

      pageNumbers.push(pageCount); // Always include the last page
    }

    return pageNumbers;
  };

  return (
    <div className="flex flex-wrap items-center justify-end space-x-2 py-4">
      {totalRecords !== 0 && (
        <div className="text-sm">
          Showing {(pageIndex - 1) * pageSize + 1} -{" "}
          {Math.min(pageIndex * pageSize, totalRecords)} of {totalRecords}{" "}
          records
        </div>
      )}
      {pageCount > 1 && (
        <div className="flex space-x-2">
          <Button
            variant="destructive"
            size="sm"
            onClick={() => setPage(--pageIndex)}
            disabled={pageIndex === 1}
            className="px-1"
          >
            <MdKeyboardArrowLeft size={24} />
          </Button>
          {getPaginationNumbers().map((page, idx) => (
            <button
              key={idx}
              className={`rounded-md border px-3 py-1 transition-all ${
                page === pageIndex ? "bg-red-500 text-white" : "border-gray-300"
              } ${typeof page === "number" ? "hover:bg-red-400" : "cursor-default"}`}
              onClick={() => {
                typeof page === "number" && setPage(page);
              }}
              disabled={typeof page !== "number"}
            >
              {page}
            </button>
          ))}
          <Button
            variant="destructive"
            size="sm"
            onClick={() => setPage(++pageIndex)}
            disabled={pageIndex === pageCount}
            className="px-1"
          >
            <MdKeyboardArrowRight size={24} />
          </Button>
        </div>
      )}
    </div>
  );
};

export default Pagination;

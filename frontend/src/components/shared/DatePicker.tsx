import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { cn } from "@/lib/utils";
import { format } from "date-fns";
import { Dispatch, SetStateAction, useState } from "react";
import { IoCalendar, IoClose } from "react-icons/io5";

interface DatePickerProps {
  formatDate?: string;
  setValue: Dispatch<SetStateAction<Date | null>>;
  placeholder?: string;
}

export function DatePicker(props: Readonly<DatePickerProps>) {
  const { formatDate, setValue, placeholder } = props;
  const [date, setDate] = useState<Date>();

  const handleDateSelect = (selectedDate: Date | undefined) => {
    setDate(selectedDate);

    if (selectedDate) {
      const nextDay = new Date(selectedDate);
      nextDay.setDate(nextDay.getDate() + 1);
      setValue(nextDay);
    } else {
      setValue(null);
    }
  };

  const handleClear = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.stopPropagation(); // Prevent the Popover from opening
    setDate(undefined);
    setValue(null);
  };

  return (
    <Popover>
      <PopoverTrigger asChild>
        <Button
          variant={"outline"}
          className={cn(
            "w-48 items-center justify-between font-normal",
            !date && "text-muted-foreground",
          )}
        >
          {date ? (
            <>
              {format(date, formatDate ?? "dd/MM/yyyy")}
              <Button
                variant="ghost"
                size="icon"
                className="h-4 w-4 p-0"
                onClick={handleClear}
              >
                <IoClose size={14} />
              </Button>
            </>
          ) : (
            <span>{placeholder || "Select date"}</span>
          )}
          <IoCalendar size={20} />
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-auto p-0">
        <Calendar
          mode="single"
          selected={date}
          onSelect={handleDateSelect}
          initialFocus
        />
      </PopoverContent>
    </Popover>
  );
}

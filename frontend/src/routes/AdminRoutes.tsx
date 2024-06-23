import { CreateAsset, CreateUser, Home, ManageUser } from "@/pages";
import { EditAsset } from "@/pages/admin/manage/asset/EditAsset";
import { ManageAsset } from "@/pages/admin/manage/asset/ManageAsset";
import { EditUser } from "@/pages/admin/manage/user/EditUser";
import { Navigate, useRoutes } from "react-router-dom";

export const AdminRoutes = () => {
  const elements = useRoutes([
    {
      path: "/",
      element: <Navigate to="/home" />,
    },
    {
      path: "/home",
      element: <Home />,
    },
    {
      path: "/users",
      element: <ManageUser />,
    },
    {
      path: "/users/create",
      element: <CreateUser />,
    },
    {
      path: "/users/edit/:staffCode",
      element: <EditUser />,
    },
    {
      path: "assets",
      element: <ManageAsset />,
    },
    {
      path: "assets/create",
      element: <CreateAsset />,
    },
    {
      path: "assets/edit/:assetCode",
      element: <EditAsset />,
    },
    {
      path: "*",
      element: <Navigate to="/notfound" />,
    },
  ]);
  return elements;
};
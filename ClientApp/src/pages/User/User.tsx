import React, { useEffect, useState } from "react";
import { Typography, Box, Tooltip, IconButton, Grid, Button, TextField } from "@mui/material";
import EditNoteTwoTone from "@mui/icons-material/EditNoteTwoTone";
import { globalStyle } from "../../styles/theme";
import DeleteTwoTone from "@mui/icons-material/DeleteTwoTone";
import Table from "../../components/Table/Table";
import PaginationControls from "../../components/Pagination/PaginationControls";
import { PaginatedList, UserDTO } from "../../models/UserDTO";
import ToastService from "../../utils/toast";
import ConfirmationModal from "../../components/Modal/ConfirmationModal";
import UserService from "../../services/UserService";


const User: React.FC = () => {
  const [id, setId] = useState(0);
  const [pagedResult, setPagedResult] = useState<PaginatedList<UserDTO>>({
    data: [],
    pageIndex: 1,
    totalPages: 0,
    countData: 0,
    hasPreviousPage: false,
    hasNextPage: false,
  });
  const [formData, setFormData] = useState<UserDTO>({
    id: 0,
    userId: '',
    firstName: '',
    lastName: '',
    email: '',
    mobileNo: '',
    createId: undefined,
    createDate: undefined,
    updateId: undefined,
    updateDate: undefined,
  });
  const [formError, setformError] = useState<any>({}); // To store error messages
  const [isSaveDisabled, setIsSaveDisabled] = useState(true);
  const [isAddDisabled, setIsAddDisabled] = useState(false);
  const [deleteOpenConfirmModal, setDeleteOpenConfirmModal] = useState(false);

  const GetList = async () => {
    try {
      const result = await UserService.getlist(pagedResult.pageIndex);
      var x = result.data;
      setPagedResult({
        pageIndex: x.pageIndex,
        totalPages: x.totalPages,
        countData: x.countData,
        hasPreviousPage: x.hasPreviousPage,
        hasNextPage: x.hasNextPage,
        data: x.data,
      });
    } catch (error) {
      console.error("Error fetching users", error);
    }
  };
  const handlePageChange = (newPage: number) => {
    setPagedResult({
      ...pagedResult,
      pageIndex: newPage,
    });
  };
  const ClearForm = () => {
    setPagedResult((prevState) => ({
      ...prevState, // Retain the other properties
      pageIndex: 1, // Update only the pageIndex
    }));
    setFormData({
      id: 0,
      userId: '',
      firstName: '',
      lastName: '',
      email: '',
      mobileNo: '',
      createId: undefined,
      createDate: undefined,
      updateId: undefined,
      updateDate: undefined,
    });
    setIsAddDisabled(false); 
    setIsSaveDisabled(true);
  }
  const AddOnClick = () => {
    setId(0);
    ClearForm();
    setIsAddDisabled(true); 
    setIsSaveDisabled(false);
  }
  const EditOnClick = (data: UserDTO) => {
    setId(data.id);
    setIsAddDisabled(true); 
    setIsSaveDisabled(false);
    setFormData({
        id: data.id,
        userId: data.userId,
        firstName: data.firstName,
        lastName: data.lastName,
        email: data.email,
        mobileNo: data.mobileNo,
        createId: data.createId,
        createDate: data.createDate,
        updateId: data.updateId,
        updateDate: data.updateDate,
    });
  }
  const SaveUser = async (e : React.FormEvent) => {
    e.preventDefault();
    try {
      let  result;
      console.log(id);
      if(id == 0){
        result = await UserService.AddUser(formData);
      }else{
        result = await UserService.UpdateUser(id, formData);
      }
      if (result.status == 0) {
        ClearForm();
        ToastService.success(result.message);
      } else {
        ToastService.error(result.message);
      }
    } catch (error : any) {
      const response = error.response;
        if (response.status === 400) {
          const errorlist = response.data;
          setformError(errorlist.errors); 
        } else {
          console.error('Other error', error);
        };
    } finally{
      setDeleteOpenConfirmModal(false);
    }
  }
  const DeleteOnClick = async () => {
    try {
      const result = await UserService.DeleteUser(id);
      if (result.status == 0) {
        ClearForm();
        ToastService.success(result.message);
      } else {
        ToastService.error(result.message);
      }
    } catch (error) {
      console.error("Error DeleteOnClick", error);
    } finally{
      setDeleteOpenConfirmModal(false);
    }
  };
  const DeleteOpenConfirmModal = (id : number) => {
    setId(id);
    setDeleteOpenConfirmModal(true);
  };  
  const DeleteCloseConfirmModal = () => {
    setId(0);
    setDeleteOpenConfirmModal(false);
  };
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };
  useEffect(() => {
    GetList();
  }, [pagedResult.pageIndex]);

  const columns = [
    { label: 'User ID', accessor: 'userId' },
    { label: 'First Name', accessor: 'firstName' },
    { label: 'Last Name', accessor: 'lastName' },
    { label: 'Email', accessor: 'email' },
    { label: 'Mobile No', accessor: 'mobileNo' },
    { label: 'Create ID', accessor: 'createId' },
    { label: 'Create Date', accessor: 'createDate' },
    { label: 'Update ID', accessor: 'updateId' },
    { label: 'Update Date', accessor: 'updateDate' },
    {
      label: 'Action',
      render: (data: any) => (
        <Box sx={globalStyle.buttonBox}>
          <Tooltip title="Edit">
            <IconButton color='primary' onClick={() => EditOnClick(data)}>
              <EditNoteTwoTone />
            </IconButton>
          </Tooltip>
          <Tooltip title="Delete" onClick={() => DeleteOpenConfirmModal(data.id)}>
            <IconButton sx={globalStyle.buttonRed}>
              <DeleteTwoTone />
            </IconButton>
          </Tooltip>
        </Box>
      ),
    },
  ];

 
  return (
    <>
      <Typography variant="h6" component="h6" gutterBottom>
        User Dashboard
      </Typography>

      <form onSubmit={SaveUser}>
        <Grid container spacing={2}>          
          <TextField
              name="id"
              value={formData.id}
              style={{ display: 'none' }} 
              onChange={handleChange}
            />
          <Grid item xs={4}>
            <TextField
              label="User ID"
              name="userId"
              value={formData.userId}
              disabled={isSaveDisabled}
              onChange={handleChange}
              error={Boolean(formError.UserId)} 
              helperText={formError.UserId ? formError.UserId[0] : ''}
              fullWidth
            />
          </Grid>

          <Grid item xs={4}>
            <TextField
              label="First Name"
              name="firstName"
              value={formData.firstName}
              onChange={handleChange}
              disabled={isSaveDisabled}
              error={Boolean(formError.FirstName)} 
              helperText={formError.FirstName ? formError.FirstName[0] : ''}
              fullWidth
            />
          </Grid>

          <Grid item xs={4}>
            <TextField
              label="Last Name"
              name="lastName"
              value={formData.lastName}
              onChange={handleChange}
              disabled={isSaveDisabled}
              error={Boolean(formError.LastName)} 
              helperText={formError.LastName ? formError.LastName[0] : ''}
              fullWidth
            />
          </Grid>

          <Grid item xs={6}>
            <TextField
              label="Email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              disabled={isSaveDisabled}
              error={Boolean(formError.Email)} 
              helperText={formError.Email ? formError.Email[0] : ''}
              fullWidth
            />
          </Grid>

          <Grid item xs={6}>
            <TextField
              label="Mobile Number"
              name="mobileNo"
              value={formData.mobileNo || ''}
              onChange={handleChange}
              disabled={isSaveDisabled}
              error={Boolean(formError.MobileNo)} 
              helperText={formError.MobileNo ? formError.MobileNo[0] : ''}
              fullWidth
              type="tel"
            />
          </Grid>
          <Grid item xs={3}>
            <Button 
            variant="contained" 
            disabled={isSaveDisabled}
            color="primary" 
            type="submit" 
            fullWidth>
              Save
            </Button>
          </Grid>
          <Grid item xs={3}>
            <Button 
            variant="contained"
            disabled={isAddDisabled}
            color="success" 
            type="button" 
            onClick={AddOnClick} 
            fullWidth>
              Add
            </Button>
          </Grid>
          <Grid item xs={3}>
            <Button variant="contained" color="warning" type="button" onClick={ClearForm} fullWidth>
              Clear
            </Button>
          </Grid>
        </Grid>
          <Box sx={globalStyle.mainBox}>
            <Box sx={{ m: 1 }}>
            </Box>
          </Box>
      </form>

      <Table columns={columns} data={pagedResult.data} />
      
      <PaginationControls
        currentPage={pagedResult.pageIndex}
        totalPages={pagedResult.totalPages ?? 0}
        onPageChange={handlePageChange}
        totalItems={pagedResult.countData}
      />

    <ConfirmationModal
        open={deleteOpenConfirmModal}
        handleClose={DeleteCloseConfirmModal}
        handleConfirm={DeleteOnClick}
        title="Are you sure?"
        content=""
        buttonName="Delete"
        id={0}
      >
    </ConfirmationModal>
    </>
  );
};

export default User;
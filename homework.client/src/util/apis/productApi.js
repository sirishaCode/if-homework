import axios from "axios";
// api for fetching the productList
export const fetchProducts = async (url) => {
    try {
      const response = await axios.get(url); 
      return response.data; 
  
    } catch (error) {
      console.error("Error while fetching products:", error);
      throw error; 
  }
}
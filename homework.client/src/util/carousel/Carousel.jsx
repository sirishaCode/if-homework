import React from 'react';
import Slider from 'react-slick';
import 'slick-carousel/slick/slick.css'; 
import 'slick-carousel/slick/slick-theme.css';
import './carousel.css';

const Carousel = ({ images, index }) => {
  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: false, // Disable arrows if you only want dots
    appendDots: dots => (
      <div style={{ marginTop: '20px' }}>
        <ul style={{ margin: '0px' }}>{dots}</ul>
      </div>
    ),
    responsive: [
      {
        breakpoint: 768, // Tablet and smaller screens
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
        },
      },
      {
        breakpoint: 480, // Mobile screens
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
        },
      },
    ],
  };

  // Default alt text handling if `image` is a URL
  const getAltText = (image, index) => {
    if (typeof image === 'string') {
      return `Image ${index + 1}`; // Use index-based description for URLs
    } else {
      return image.alt || `Image ${index + 1}`; // Fallback to a generic alt if no alt is available
    }
  };

  return (
    <div className="carousel-container" aria-label="Product images carousel">
      {images.length > 1 ? (
        // Show Carousel if there are multiple images
        <Slider {...settings}>
          {images.map((image, index) => 
          (
            <div key={index} className="carousel-image-slide">
              <img
                src={image}
                alt={getAltText(image, index)}  // Improved alt text handling
                className="carousel-image square-box"
              />
            </div>
          ))}
        </Slider>
      ) : (
        // Show a single image if only one is available
        <div className="single-image-container">
          <img
            src={images[0]}
            alt={getAltText(images[0], 0)} // Single image alt text
            className="carousel-image square-box"
          />
        </div>
      )}
    </div>
  );
};

export default Carousel;

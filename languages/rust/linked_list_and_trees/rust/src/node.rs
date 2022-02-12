#![allow(dead_code)]

// #![allow(unused_variables)]

#[derive(Debug, PartialEq)]
pub struct Node<'a> {
    pub value: i32,
    // pub next: Option<Box<Node>>,
    // pub next: Option<*const Node>,
    pub next: Option<&'a Node<'a>>,
}

// impl Node {
//     pub fn to_vec(&self) -> Vec<i32> {
//         let mut vec = vec![self.value];
//         if let Some(next) = &self.next {
//             let mut temp = next.to_vec();
//             vec.append(&mut temp);
//         };
//         vec
//     }

//     pub fn find(&self, value: i32) -> Option<&Node> {
//         if self.value == value {
//             return Some(self);
//         }

//         if let Some(next) = &self.next {
//             return next.find(value);
//         }

//         return None;
//     }

//     fn to_vec_ref(&self) -> Vec<&Node> {
//         let mut vec = vec![self];
//         if let Some(next) = &self.next {
//             let mut sub_vec = next.to_vec_ref();
//             vec.append(&mut sub_vec);
//         }
//         vec
//     }

//     pub fn reverse(&self) -> &Node {
//         let vec_ref = self.to_vec_ref();
//         for (i, node) in vec_ref.iter().enumerate().rev() {
//             //
//             dbg!(i, node.value);
//         }
//         self
//     }
// }

// pub fn get_test_list<'a>(size: i32, current: i32) -> Node<'a> {
//     let mut node = Node {
//         value: current,
//         next: None,
//     };
//     if current < size {
//         let next = get_test_list(size, current + 1);
//         // node.next = Some(Box::from(next));
//         node.next = Some(&next);
//     }
//     node
// }

// #[cfg(test)]
// mod tests {
//     use crate::node::*;

//     #[test]
//     fn test_to_vec() {
//         let root = get_test_list(5, 1);
//         let vec = root.to_vec();
//         assert_eq!(vec.len(), 5);
//         assert_eq!(vec[0], 1);
//         assert_eq!(vec[1], 2);
//         assert_eq!(vec[2], 3);
//         assert_eq!(vec[3], 4);
//         assert_eq!(vec[4], 5);

//         println!("test_to_vec succeeded");
//     }

//     #[test]
//     fn test_find() {
//         let root = get_test_list(5, 1);

//         assert_eq!(root.find(1).unwrap().value, 1);
//         assert_eq!(root.find(3).unwrap().value, 3);
//         assert_eq!(root.find(0), None);

//         println!("test_find succeeded");
//     }

//     #[test]
//     fn test_reverse() {
//         let root = get_test_list(5, 1);
//         let reversed = root.reverse();
//         let vec = reversed.to_vec();
//         // dbg!(vec);
//         println!("{:?}", vec);
//     }
// }

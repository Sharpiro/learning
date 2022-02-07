#![allow(dead_code)]
// #![allow(unused_variables)]

#[derive(Debug)]
pub struct Node {
    pub value: u8,
    pub next: Option<Box<Node>>,
}

impl Node {
    pub fn to_vec(&self) -> Vec<u8> {
        let mut vec = vec![self.value];
        if let Some(next) = &self.next {
            let mut temp = next.to_vec();
            vec.append(&mut temp);
        };
        vec
    }
}

// fn get_test_list() -> Node {
//     let root = Node {
//         value: 1,
//         next: Some(Box::from(Node {
//             value: 2,
//             next: Some(Box::from(Node {
//                 value: 3,
//                 next: Some(Box::from(Node {
//                     value: 4,
//                     next: Some(Box::from(Node {
//                         value: 5,
//                         next: None,
//                     })),
//                 })),
//             })),
//         })),
//     };
//     root
// }

fn get_test_list_dyn(size: u8, current: u8) -> Node {
    let mut node = Node {
        value: current,
        next: None,
    };
    if current < size {
        let next = get_test_list_dyn(size, current + 1);
        node.next = Some(Box::from(next));
    }
    node
}

#[cfg(test)]
mod tests {
    use crate::node::*;

    #[test]
    fn test_to_vec() {
        let root = get_test_list_dyn(5, 1);
        let vec = root.to_vec();
        assert_eq!(vec.len(), 5);
        assert_eq!(vec[0], 1);
        assert_eq!(vec[1], 2);
        assert_eq!(vec[2], 3);
        assert_eq!(vec[3], 4);
        assert_eq!(vec[4], 5);
    }
}
